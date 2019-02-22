using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;
using UsersManager.Services;
using Xunit;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    [Collection(ApiCollectionFixture.Name)]
    public class UserTests: IClassFixture<ApiFixture>
    {
        private readonly ApiFixture _context;
        private ITestOutputHelper _output;
        
        public UserTests(ApiFixture fixture, ITestOutputHelper helper)
        {
            _context = fixture;
            _output = helper;
        }

        
        [Fact]
        public async void UserCanGetCorrectProfile()
        {
            var correctProfile = new UserProfile()
            {
                FirstName = "Jhon",
                LastName = "Foster",
                NickName = "JFoster",
                Email = "jfoster@gmail.com",
                PhoneNumber = 89129541254L,
                Description = "American"
            };

            var request = new UserCredentials()
            {
                Nickname = "JFoster",
                Password = "1"
            };

            await _context.AuthorizeAsUser();
            
            var response = await _context.Client.GetAsync("account/profile/1");
            var body = await response.Content.ReadAsStringAsync();

            var profile = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(body);
            
            response.StatusCode.Should().BeEquivalentTo(200);
            profile.Should().BeEquivalentTo(correctProfile);
        }
        [Fact]
        public async void AdminCanGetCorrectUser()
        {
            var correctUser = new User()
            {
                Id = 1,
                Guid = Guid.Parse("26a709ce-ce27-43ba-a7d4-6e05ca5368f8"),
                FirstName = "Jhon",
                LastName = "Foster",
                NickName = "JFoster",
                Email = "jfoster@gmail.com",
                PhoneNumber = 89129541254L,
                Description = "American",
                InvitedAt = DateTime.Parse("2014-12-15"),
                DepartmentId = 1,
                Department = new Department()
                {
                    Id = 1,
                    Name = "developer"
                }
            };
            
            await _context.AuthorizeAsManager();

            var user = await GetUser(1);
            
            user.Should().BeEquivalentTo(correctUser);
        }
        [Fact]
        public async void AdminCanCreateUser()
        {
            await _context.AuthorizeAsAdmin();
            
            var user = await CreateUser();
            
            var newUser = await GetUser(user.Id);
            
            newUser.Should().NotBeNull();
            
            user.Department = new Department()
            {
                Id = 2,
                Name = "manager"
            };
            
            newUser.Should().BeEquivalentTo(user);
            
            await RemoveUser(4);
            
            var response = await _context.Client.GetAsync("account/user/4");
            var body = await response.Content.ReadAsStringAsync();
            
            body.Should().Contain("\"message\":\"No user with id '4'\"");
        }
        [Fact]
        public async void ManagerCanModifyUser()
        {
            var newProfile = new UserProfile()
            {
                FirstName = "Jhony",
                LastName = "Foster",
                NickName = "JFoster",
                Email = "jfoster@gmail.com",
                PhoneNumber = 89129541254L,
                Description = "Azia"
            };

            await _context.AuthorizeAsManager();
            
            var response = await _context.Client.PostAsJsonAsync("account/modify/1", newProfile);
            
            response.StatusCode.Should().BeEquivalentTo(200);

            var newUser = await GetUser(1);

            newUser.Should().NotBeNull();
            newUser.FirstName.Should().BeEquivalentTo(newProfile.FirstName);
            newUser.Description.Should().BeEquivalentTo(newProfile.Description);


            newProfile.FirstName = "Jhon";
            newProfile.Description = "American";
            
            response = await _context.Client.PostAsJsonAsync("account/modify/1", newProfile);
            
            response.StatusCode.Should().BeEquivalentTo(200);

            var oldUser = await GetUser(1);

            oldUser.Should().NotBeNull();
            oldUser.FirstName.Should().BeEquivalentTo(newProfile.FirstName);
            oldUser.Description.Should().BeEquivalentTo(newProfile.Description);
        }
        [Fact]
        public async void AdminCanAssignUserToManager()
        {
            var umId = new UserManagerId()
            {
                UserId = 4,
                ManagerId = 2
            };

            await _context.AuthorizeAsAdmin();
            await CreateUser();            
            
            var response = await _context.Client.PostAsJsonAsync("account/assign", umId);
            
            var user = await GetUser(4);
            user.ManagerId.Should().Be(2);

            await RemoveUser(4);
            
            response.StatusCode.Should().BeEquivalentTo(200);
        }

        
        private async Task<User> CreateUser()
        {
            var user = new User()
            {
                Id = 4,
                Guid = Guid.NewGuid(),
                FirstName = "Jho",
                LastName = "Fal",
                NickName = "Kio",
                Email = "kil@gmail.com",
                PhoneNumber = 89139541254L,
                Description = "American",
                InvitedAt = DateTime.Parse("2014-11-15"),
                DepartmentId = 2
            };
            
            var req = new UserCreationRequest()
            {
                User = user,
                Password = "1234"
            };
            
            var response = await _context.Client.PostAsJsonAsync("account/create", req);
            var body = await response.Content.ReadAsStringAsync();
            
            response.StatusCode.Should().BeEquivalentTo(200);

            return user;
        }
        private async Task<User> GetUser(int id)
        {
            var response = await _context.Client.GetAsync($"account/user/{id}");
            var body = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().BeEquivalentTo(200);

            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(body);
            
            return user;
        }
        private async Task RemoveUser(int id)
        {
            var response = await _context.Client.PostAsync("account/remove/4", null);
            
            response.StatusCode.Should().BeEquivalentTo(200);
        }
    }

}
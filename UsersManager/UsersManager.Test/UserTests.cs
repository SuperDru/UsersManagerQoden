using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
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
        private readonly ITestOutputHelper _output;
        
        public UserTests(ApiFixture fixture, ITestOutputHelper helper)
        {
            _context = fixture;
            _output = helper;
        }

        
        [Fact]
        public async void UserCanGetCorrectProfile()
        {
            await _context.AuthorizeAsUser();
            
            var response = await _context.Client.GetAsync("account/profile");
            var profile = await response.Content.ReadAsAsync<UserProfile>();
            
            response.StatusCode.Should().BeEquivalentTo(200);
            profile.NickName.Should().BeEquivalentTo("JFoster");
            profile.PhoneNumber.Should().Be(89129541254L);
        }
        [Fact]
        public async void AdminCanGetCorrectUser()
        {   
            await _context.AuthorizeAsAdmin();

            var response = await _context.Client.GetAsync("account/user/1");
            var user = await response.Content.ReadAsAsync<User>();

            response.StatusCode.Should().BeEquivalentTo(200);
            
            user.NickName.Should().BeEquivalentTo("JFoster");
            user.InvitedAt.Should().Be(DateTime.Parse("2014-12-15"));
        }
        [Fact]
        public async void AdminCanCreateUser()
        {
            await _context.AuthorizeAsAdmin();
            
            var req = new UserCreationRequest()
            {
                FirstName = "Jho",
                LastName = "Fal",
                NickName = "Kio",
                Email = "kil@gmail.com",
                PhoneNumber = 89139541254L,
                Description = "American",
                InvitedAt = DateTime.Parse("2014-11-15"),
                DepartmentId = 2,
                Password = "1234"
            };
            
            var response = await _context.Client.PostAsJsonAsync("account/create", req);
            response.StatusCode.Should().BeEquivalentTo(200);
            
            var newUser = await _context.GetUser(4);
            
            newUser.Should().NotBeNull();
            newUser.NickName.Should().BeEquivalentTo(req.NickName);
            newUser.InvitedAt.Should().Be(req.InvitedAt);
            
            await _context.RemoveUser(4);
        }
        [Fact]
        public async void ManagerCanModifyUser()
        {
            await _context.CreateUser();
            var newProfile = new UserProfile()
            {
                FirstName = "Jhon",
                LastName = "Fal",
                NickName = "Kio",
                Email = "kil@gmail.com",
                PhoneNumber = 89139541254L,
                Description = "Asia",
            };

            await _context.AuthorizeAsManager();
            
            var response = await _context.Client.PostAsJsonAsync("account/modify/4", newProfile);
            
            response.StatusCode.Should().BeEquivalentTo(200);

            var newUser = await _context.GetUser(4);
            _output.WriteLine(newUser.Description);
            await _context.RemoveUser(4);

            newUser.Should().NotBeNull();
            newUser.FirstName.Should().BeEquivalentTo(newProfile.FirstName);
            newUser.Description.Should().BeEquivalentTo(newProfile.Description);
        }
        [Fact]
        public async void AdminCanAssignUserToManager()
        {
            var umId = new AssignUserToManagerRequest()
            {
                UserId = 4,
                ManagerId = 2
            };

            await _context.AuthorizeAsAdmin();
            await _context.CreateUser();            
            
            var response = await _context.Client.PostAsJsonAsync("account/assign", umId);
            
            response.StatusCode.Should().BeEquivalentTo(200);
            
            var user = await _context.GetUser(4);
            await _context.RemoveUser(4);
            
            user.ManagerId.Should().Be(2);
        }
    }
}
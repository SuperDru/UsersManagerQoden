using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using Xunit;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    [Collection(ApiCollectionFixture.Name)]
    public class SalaryRatesTests: IClassFixture<ApiFixture>
    {
        private readonly ApiFixture _context;
        private ITestOutputHelper _output;

        public SalaryRatesTests(ApiFixture fixture, ITestOutputHelper helper)
        {
            _context = fixture;
            _output = helper;
        }

        [Fact]
        public async void UserCanGetSalaryRateRequest()
        {
            await _context.AuthorizeAsUser();

            var response = await _context.Client.GetAsync("salary/requests/1");
            var body = await response.Content.ReadAsStringAsync();

            var requestsList = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<SalaryRateRequest>>(body);

            response.StatusCode.Should().BeEquivalentTo(200);
            requestsList.First().UpdatedAt.Should().Be(DateTime.Parse("2017-05-11"));
            requestsList.Last().UpdatedAt.Should().Be(DateTime.Parse("2017-05-08"));
        }
        
        
        [Fact]
        public async void UserCanCreateSalaryRateRequest()
        {
            await _context.AuthorizeAsAdmin();

            await CreateUser();
            
            var request = new UserRateRequestRequest()
            {
                UserId = 4,
                Rate = 4000,
                Reasons = "I'm good manager",
                UpdatedAt = DateTime.Now
            };
            
            var response = await _context.Client.PostAsJsonAsync("salary/request", request);
            
            response.StatusCode.Should().BeEquivalentTo(200);
            
            response = await _context.Client.GetAsync("salary/requests/4");
            var body = await response.Content.ReadAsStringAsync();

            var requestsList = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<SalaryRateRequest>>(body);

            var req = requestsList.First();

            req.Reasons.Should().Be("I'm good manager");

            await RemoveUser(4);
        }

        [Fact]
        public async void ManagerCanGetRequests()
        {
            await _context.AuthorizeAsManager();

            var response = await _context.Client.GetAsync("salary/requests");
            var body = await response.Content.ReadAsStringAsync();

            var requestsList = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<ManagerRateRequestAnswer>>(body);

            response.StatusCode.Should().BeEquivalentTo(200);
            foreach (var req in requestsList)
            {
                req.Reasons.Should().BeEquivalentTo("Just so");
            }
        }

        [Fact] public async void AdminCanGetAllRequests()
        {
            await _context.AuthorizeAsAdmin();

            var response = await _context.Client.GetAsync("salary/all-requests");
            var body = await response.Content.ReadAsStringAsync();

            var requestsList = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<SalaryRateRequest>>(body);

            response.StatusCode.Should().BeEquivalentTo(200);
            requestsList.Count.Should().Be(3);
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
        
        private async Task RemoveUser(int id)
        {
            var response = await _context.Client.PostAsync($"account/remove/{id}", null);
            
            response.StatusCode.Should().BeEquivalentTo(200);
        }
    }
}
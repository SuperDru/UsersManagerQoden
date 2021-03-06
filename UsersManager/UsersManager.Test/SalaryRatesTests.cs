using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
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
            var requestsList = await response.Content.ReadAsAsync<ICollection<UserRateRequestAnswer>>();

            response.StatusCode.Should().BeEquivalentTo(200);
            requestsList.First().UpdatedAt.Should().Be(DateTime.Parse("2017-05-11"));
            requestsList.Last().UpdatedAt.Should().Be(DateTime.Parse("2017-05-08"));
        }
        
        
        [Fact]
        public async void UserCanCreateSalaryRateRequest()
        {
            await _context.AuthorizeAsAdmin();

            await _context.CreateUser();

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
            var requestsList = await response.Content.ReadAsAsync<ICollection<UserRateRequestAnswer>>();

            var req = requestsList.First();

            await _context.RemoveUser(4);
            
            req.Reasons.Should().Be(request.Reasons);
            req.Rate.Should().Be(request.Rate);
            req.UpdatedAt.Should().BeCloseTo(request.UpdatedAt);
        }

        [Fact]
        public async void ManagerCanGetRequests()
        {
            await _context.AuthorizeAsManager();

            var response = await _context.Client.GetAsync("salary/requests");
            var requestsList = await response.Content.ReadAsAsync<ICollection<ManagerRateRequestAnswer>>();

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
            var requestsList = await response.Content.ReadAsAsync<ICollection<SalaryRateRequest>>();

            response.StatusCode.Should().BeEquivalentTo(200);
            requestsList.Count.Should().Be(3);
        }
        
    }
}
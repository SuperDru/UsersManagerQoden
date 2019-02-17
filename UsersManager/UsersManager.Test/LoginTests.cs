using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using UsersManager.DtoModels;
using Xunit;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    [Collection(ApiCollectionFixture.Name)]
    public class LoginTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture _context;
        private ITestOutputHelper _output;
        
        public LoginTests(ApiFixture fixture, ITestOutputHelper output)
        {
            _context = fixture;
            _output = output;
        }
        
        [Fact]
        public async void UserCanLogin()
        {
            var request = new UserCredentials()
            {
                Nickname = "AShishkin",
                Password = "12"
            };
            
            var response = await _context.Client.PostAsJsonAsync("account/login", request);
            
            response.StatusCode.Should().BeEquivalentTo(200);
            response.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value.Should().NotBeNull();
        }
        
        [Theory]
        [InlineData("AShishkin", "12345")]
        [InlineData("AShishkind", "54321")]
        [InlineData("AShishkinf", "12")]
        public async Task UserWithInvalidCredentialsCannotLogin(string nickname, string password)
        {
            var request = new UserCredentials()
            {
                Nickname = nickname,
                Password = password
            };
            
            var response = await _context.Client.PostAsJsonAsync("account/login", request);
            var body = await response.Content.ReadAsStringAsync();
            _output.WriteLine(body);
            
            response.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value.Should().BeNull();
            body.Should().Contain("\"message\":\"Invalid username or password\"");
            response.StatusCode.Should().BeEquivalentTo(400);
        }
    }
}
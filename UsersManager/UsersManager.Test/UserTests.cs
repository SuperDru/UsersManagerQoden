using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using Xunit;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    [Collection(ApiCollectionFixture.Name)]
    public class UserTests: IClassFixture<ApiFixture>
    {
        private ApiFixture _context;
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
            
            var clientLogin = await _context.Client.PostAsJsonAsync("account/login", request);
            var cookie = clientLogin.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value;
            _context.Client.DefaultRequestHeaders.Add("Cookie", cookie);
            
            var response = await _context.Client.GetAsync("account/profile/1");
            var body = await response.Content.ReadAsStringAsync();

            var profile = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(body);
            
            response.StatusCode.Should().BeEquivalentTo(200);
            profile.Should().BeEquivalentTo(correctProfile);
        }
        

    }

}
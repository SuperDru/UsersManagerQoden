using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    public class ApiFixture: IDisposable
    {
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }
        public CompanyDbContext DbContext => Server.Host.Services.GetService<CompanyDbContext>();
        
        public ApiFixture()
        {
            Server = new TestServer(SetupWebHost());
            Client = Server.CreateClient();
        }
        
        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
        
        private static IWebHostBuilder SetupWebHost()
        {
            return new WebHostBuilder()
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    //builder.AddJsonFile("Configuration/config.json");
                    builder.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
        }

        public async Task AuthorizeAsUser()
        {
            var request = new UserCredentials()
            {
                Nickname = "JFoster",
                Password = "1"
            };
            
            var clientLogin = await Client.PostAsJsonAsync("account/login", request);
            var cookie = clientLogin.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value;
            Client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        
        public async Task AuthorizeAsManager()
        {
            var request = new UserCredentials()
            {
                Nickname = "AShishkin",
                Password = "12"
            };
            
            var clientLogin = await Client.PostAsJsonAsync("account/login", request);
            var cookie = clientLogin.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value;
            Client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        
        public async Task AuthorizeAsAdmin()
        {
            var request = new UserCredentials()
            {
                Nickname = "AShurikov",
                Password = "123"
            };
            
            var clientLogin = await Client.PostAsJsonAsync("account/login", request);
            var cookie = clientLogin.Headers.FirstOrDefault(h => h.Key == "Set-Cookie").Value;
            Client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        
        public async Task<User> CreateUser()
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

            DbContext.Users.Add(user);
            
            var salt = PasswordGenerator.GenerateSalt();
            var pass = PasswordGenerator.HashPassword("1234", salt);
            DbContext.Credentials.Add(new HashedCredential()
            {
                UserId = user.Id,
                Salt = salt,
                HashedPassword = pass
            });
            
            await DbContext.SaveChangesAsync();

            return user;
        }
        public async Task<User> GetUser(int id)
        {
            var response = await Client.GetAsync($"account/user/{id}");
            response.StatusCode.Should().BeEquivalentTo(200);
            
            return await response.Content.ReadAsAsync<User>();
        }
        public async Task RemoveUser(int id)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            var cred = await DbContext.Credentials.FirstOrDefaultAsync(u => u.UserId == id);
            var s = await DbContext.SalaryRateRequests.FirstOrDefaultAsync(u => u.UserId == id);
            
            if (user != null) DbContext.Users.Remove(user);
            if (cred != null) DbContext.Credentials.Remove(cred);
            if (s != null) DbContext.SalaryRateRequests.Remove(s);
            
            await DbContext.SaveChangesAsync();
        }
    }
}
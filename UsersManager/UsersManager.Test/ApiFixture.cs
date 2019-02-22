using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using UsersManager.DtoModels;
using Xunit.Abstractions;

namespace UsersManager.Test
{
    public class ApiFixture: IDisposable
    {
        public TestServer Server { get; set; }

        public HttpClient Client { get; set; }

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
    }
}
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

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
    }
}
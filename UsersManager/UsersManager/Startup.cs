using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;
using UsersManager.Services;

namespace UsersManager
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISalaryRatesService, SalaryRatesService>();
            services.AddScoped<IAccountService, AccountService>();

            ConfigureDatabase(services);
            ConfigureAutoMapper(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<CompanyDbContext>((provider, options) =>
            {
                options.UseNpgsql(Configuration["Database:ConnectionString"]);
            }); 
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRateRequestRequest, SalaryRateRequest>();
                cfg.CreateMap<SalaryRateRequest, UserRateRequestAnswer>();
                cfg.CreateMap<SalaryRateRequest, ManagerRateRequestAnswer>();
                cfg.CreateMap<User, UserProfile>();
                cfg.CreateMap<UserProfile, User>();
            }).CreateMapper();
            
            services.AddSingleton(mapper);
        }
    }
}
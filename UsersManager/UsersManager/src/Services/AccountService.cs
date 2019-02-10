using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;

namespace UsersManager.Services
{
    public interface IAccountService
    {
        Task<bool> CheckPassword(User user, string password);
        ClaimsPrincipal Login(User user);
    }
    
    public class AccountService : IAccountService
    {
        private readonly CompanyDbContext _dbContext;

        public AccountService(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<bool> CheckPassword(User user, string password)
        {
            var cred = await _dbContext.Credentials.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var hashedPassword = PasswordGenerator.HashPassword(password, cred.Salt);

            return hashedPassword == cred.HashedPassword;
        }

        public ClaimsPrincipal Login(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.AddRange(user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));
                
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
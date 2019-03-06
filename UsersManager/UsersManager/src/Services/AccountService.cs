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
using Qoden.Validation;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;

namespace UsersManager.Services
{
    public interface IAccountService
    {
        Task<ClaimsPrincipal> Login(UserCredentials cred);
    }
    
    public class AccountService : IAccountService
    {
        private readonly CompanyDbContext _dbContext;
        private readonly IUserRepository _rep;
        
        public AccountService(CompanyDbContext dbContext, IUserRepository rep)
        {
            _dbContext = dbContext;
            _rep = rep;
        }

        public async Task<ClaimsPrincipal> Login(UserCredentials cred)
        {
            var user = await _rep.GetUserByNickname(cred.Nickname);
            Check.Value(user, "credentials").NotNull(ErrorMessages.CredentialsMsg);

            var correct = await CheckPassword(user.Id, cred.Password);
            Check.Value(correct, "credentials").IsTrue(ErrorMessages.CredentialsMsg);

            return await GetClaimsPrincipal(user);
        }
        
        private async Task<bool> CheckPassword(int userId, string password)
        {
            var cred = await _dbContext.Credentials.FirstOrDefaultAsync(c => c.UserId == userId);

            var hashedPassword = PasswordGenerator.HashPassword(password, cred.Salt);

            return hashedPassword == cred.HashedPassword;
        }

        private async Task<ClaimsPrincipal> GetClaimsPrincipal(User user)
        {
            var role = await _rep.GetUserRole(user.Id);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name)
            };
                
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
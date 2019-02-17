
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qoden.Validation;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;
using UsersManager.Services;

namespace UsersManager.Controllers
{   
    [Route("/account")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _rep;
        private readonly IAccountService _accountService;
        
        public LoginController(IUserRepository rep, IAccountService accountService)
        {
            _rep = rep;
            _accountService = accountService;
        }
 
        
        [HttpPost("login")]
        public async Task Login([FromBody]UserCredentials cred)
        {
            var user = await _rep.GetUserByNickname(cred.Nickname);
            Check.Value(user, "credentials").NotNull(ErrorMessages.CredentialsMsg);

            var correct = await _accountService.CheckPassword(user, cred.Password);
            Check.Value(correct, "credentials").IsTrue(ErrorMessages.CredentialsMsg);

            await HttpContext.SignInAsync(_accountService.Login(user));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        
    }
}
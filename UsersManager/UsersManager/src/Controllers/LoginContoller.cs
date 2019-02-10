
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Services;

namespace UsersManager.Controllers
{   
    [Route("/account")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        
        public LoginController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }
 
        
        [HttpPost("login")]
        public async Task Login([FromBody]UserCredentials cred)
        {
            var user = await _userService.GetUser(cred.Nickname);

            if (user == null || !(await _accountService.CheckPassword(user, cred.Password))) 
                Response.StatusCode = 403;
            else 
                await HttpContext.SignInAsync(_accountService.Login(user));
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        
    }
}
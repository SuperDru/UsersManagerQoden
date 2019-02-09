
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
 
        
        [HttpPost("login")]
        public async Task Login([FromBody]UserCredentials cred)
        {
            var user = await _userService.GetUser(cred.Nickname);

            if (user != null && user.Password == cred.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                claims.AddRange(user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            }
            else
                Response.StatusCode = 403;
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        
    }
}
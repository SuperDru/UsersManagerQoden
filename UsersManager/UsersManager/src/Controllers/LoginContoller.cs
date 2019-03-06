using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManager.DtoModels;
using UsersManager.Services;

namespace UsersManager.Controllers
{   
    [Route("/account")]
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;
        
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }
 
        
        [HttpPost("login")]
        public async Task Login([FromBody]UserCredentials cred) => 
            await HttpContext.SignInAsync(await _accountService.Login(cred));

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout() =>
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        
    }
}
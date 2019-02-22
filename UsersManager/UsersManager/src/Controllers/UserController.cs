using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Services;

namespace UsersManager.Controllers
{ 
    [Route("account")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService service)
        {
            _userService = service;
        }


        [HttpGet("profile/{id}")]
        [Authorize]
        public async Task<UserProfile> GetProfile([FromRoute]int id) =>
            await _userService.GetUserProfile(id);

        [HttpGet("user/{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<User> GetUser([FromRoute] int id) =>
            await _userService.GetUser(id);
        
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task CreateUser([FromBody] UserCreationRequest userReq) =>
            await _userService.CreateUser(userReq.User, userReq.Password);
        
        [HttpPost("remove/{id}")]
        [Authorize(Roles = "admin")]
        public async Task RemoveUser([FromRoute] int id) =>
            await _userService.RemoveUser(id);
        
        [HttpPost("modify/{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task ModifyUser([FromRoute] int id, [FromBody] UserProfile profile) =>
            await _userService.ModifyUserProfile(id, profile);

        [HttpPost("assign")]
        [Authorize(Roles = "admin")]
        public async Task AssignUserToManager([FromBody]UserManagerId umId) =>
            await _userService.AssignUserToManager(umId.UserId, umId.ManagerId);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        
        
        public UserController(IUserService service, IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }


        [HttpGet("profile")]
        [Authorize]
        public async Task<UserProfile> GetProfile() =>
            _mapper.Map<UserProfile>(await _userService.GetUser(int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)));

        [HttpGet("user/{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<User> GetUser([FromRoute] int id) =>
            await _userService.GetUser(id);
        
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task CreateUser([FromBody] UserCreationRequest userReq) =>
            await _userService.CreateUser(userReq);
        
        [HttpPost("modify/{id}")]
        [Authorize(Roles = "admin,manager")]
        public async Task ModifyUser([FromRoute] int id, [FromBody] UserProfile profile) =>
            await _userService.ModifyUserProfile(id, profile);

        [HttpPost("assign")]
        [Authorize(Roles = "admin")]
        public async Task AssignUserToManager([FromBody]AssignUserToManagerRequest umRequest) =>
            await _userService.AssignUserToManager(umRequest.UserId, umRequest.ManagerId);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Services;

namespace UsersManager.Controllers
{
    [Route("/salary")]
    public class SalaryRatesController : Controller
    {
        private readonly ISalaryRatesService _salaryRatesService;
        
        public SalaryRatesController(ISalaryRatesService salaryRatesService)
        {
            _salaryRatesService = salaryRatesService;
        }


        [HttpPost("new-request")]
        [Authorize]
        public async Task CreateRateRequest([FromBody]UserRateRequestRequest request) =>
            await _salaryRatesService.CreateSalaryRequest(request);
        
        [HttpGet("requests/{id}")]
        [Authorize]
        public async Task<ICollection<UserRateRequestAnswer>> GetUserRateRequests([FromRoute] int id) =>
            await _salaryRatesService.GetUserSalaryRateRequests(id);
        
        [HttpGet("all-requests")]
        [Authorize(Roles = "admin")]
        public ICollection<SalaryRateRequest> GetAllRateRequests() =>
            _salaryRatesService.GetAllRateRequests();
        
        [HttpGet("requests")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ICollection<ManagerRateRequestAnswer>> GetManagerRateRequests()
        {
            int managerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            return await _salaryRatesService.GetManagerSalaryRateRequests(managerId);
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;

namespace UsersManager.Services
{
    public interface ISalaryRatesService
    {
        Task CreateSalaryRequest(UserRateRequestRequest request);
        Task<ICollection<UserRateRequestAnswer>> GetUserSalaryRateRequests(int userId);
        Task<ICollection<ManagerRateRequestAnswer>> GetManagerSalaryRateRequests(int managerId);
        Task<ICollection<SalaryRate>> GetSalaryRates(int userId);
        ICollection<SalaryRateRequest> GetAllRateRequests();
    }
    
    public class SalaryRatesService : ISalaryRatesService
    {
        private readonly CompanyDbContext _dbContext;
        private readonly IUserService _userService;
        
        public SalaryRatesService(IUserService userService, CompanyDbContext context)
        {
            _dbContext = context;
            _userService = userService;
        }
        
        public async Task CreateSalaryRequest(UserRateRequestRequest request)
        {   
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<UserRateRequestRequest, SalaryRateRequest>())
                .CreateMapper();

            var user = await _userService.GetUser(request.UserId);

            if (user != null && user.ManagerId != 0)
            {
                var req = mapper.Map<SalaryRateRequest>(request);

                req.Guid = Guid.NewGuid().ToString();
                req.ManagerId = user.ManagerId;
                req.Status = Status.Pending;

                _dbContext.SalaryRateRequests.Add(req);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<UserRateRequestAnswer>> GetUserSalaryRateRequests(int userId)
        {
            var requests = await _dbContext.SalaryRateRequests.Where(s => s.UserId == userId).ToListAsync();
            
            var mapper = new MapperConfiguration(
                    cfg => cfg.CreateMap<SalaryRateRequest, UserRateRequestAnswer>())
                .CreateMapper();
            
            return requests.ConvertAll(req => mapper.Map<UserRateRequestAnswer>(req));
        }

        public async Task<ICollection<ManagerRateRequestAnswer>> GetManagerSalaryRateRequests(int managerId)
        {
            var requests = await _dbContext.SalaryRateRequests.Where(s => s.ManagerId == managerId).ToListAsync();
            
            var mapper = new MapperConfiguration(
                    cfg => cfg.CreateMap<SalaryRateRequest, ManagerRateRequestAnswer>())
                .CreateMapper();

            return requests.ConvertAll(req => mapper.Map<ManagerRateRequestAnswer>(req));
        }

        public async Task<ICollection<SalaryRate>> GetSalaryRates(int userId)
        {
            return await _dbContext.SalaryRates.Where(u => u.UserId == userId).ToListAsync();
        }
        
        public ICollection<SalaryRateRequest> GetAllRateRequests()
        {
            return _dbContext.SalaryRateRequests.Include(s => s.Manager).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.EntityFrameworkCore;
using Qoden.Validation;
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
        private readonly IMapper _mapper;
        
        public SalaryRatesService(IUserService userService, CompanyDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _userService = userService;
            _mapper = mapper;
        }
        
        public async Task CreateSalaryRequest(UserRateRequestRequest request)
        {   
            var user = await _userService.GetUser(request.UserId);

            Check.Value(user.ManagerId, "create request").NotEqualsTo(0, ErrorMessages.NoManagerAttachmentMsg);
            
            var rateReqRecord = await _dbContext.SalaryRateRequests
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.UpdatedAt == request.UpdatedAt);

            Check.Value(rateReqRecord, "create request").IsNull(ErrorMessages.IncorrectDateUpdateMsg);

            var req = _mapper.Map<SalaryRateRequest>(request);

            req.Guid = Guid.NewGuid();
            req.ManagerId = user.ManagerId;
            req.Status = Status.Pending;
            req.UserId = user.Id;

            _dbContext.SalaryRateRequests.Add(req);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<UserRateRequestAnswer>> GetUserSalaryRateRequests(int userId)
        {
            var requests = await _dbContext.SalaryRateRequests.Where(s => s.UserId == userId).ToListAsync();
            
            return requests.ConvertAll(req => _mapper.Map<UserRateRequestAnswer>(req));
        }

        public async Task<ICollection<ManagerRateRequestAnswer>> GetManagerSalaryRateRequests(int managerId)
        {
            var requests = await _dbContext.SalaryRateRequests.Where(s => s.ManagerId == managerId).ToListAsync();

            return requests.ConvertAll(req => _mapper.Map<ManagerRateRequestAnswer>(req));
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
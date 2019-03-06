using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;
using AutoMapper;
using Qoden.Validation;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace UsersManager.Services
{
    public interface IUserService
    {
        Task ModifyUserProfile(int id, UserProfile profile);
        Task<UserProfile> GetUserProfile(int id);
        Task<User> GetUser(int id);
        Task<User> GetUser(string nickname);
        Task CreateUser(User user, string password);
        Task AssignUserToManager(int userId, int manageId);
        Task RemoveUser(int id);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _rep;
        private readonly CompanyDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(IUserRepository rep, CompanyDbContext context, IMapper mapper)
        {
            _rep = rep;
            _dbContext = context;
            _mapper = mapper;
        }

        
        public async Task<UserProfile> GetUserProfile(int id)
        {
            var user = await _rep.GetUserById(id);

            Check.Value(user, "get user").NotNull(ErrorMessages.NoUserWithIdMsg(id));
            
            return _mapper.Map<UserProfile>(user);
        }
        
        public async Task<User> GetUser(int id)
        {
            var user = await _rep.GetUserById(id);
            
            Check.Value(user, "get user").NotNull(ErrorMessages.NoUserWithIdMsg(id));
            
            return user;
        }
        
        public async Task<User> GetUser(string nickname)
        {
            var user = await _rep.GetUserByNickname(nickname);
            
            Check.Value(user, "get user").NotNull(ErrorMessages.NoUserWithNicknameMsg(nickname));
            
            return user;
        }

        public async Task CreateUser(User user, string password)
        {
            var userChecking = await _rep.GetUserByNickname(user.NickName);
            Check.Value(userChecking, "create user").IsNull(ErrorMessages.UserWithNicknameExistsMsg(user.NickName));
            Check.Value(password, "password").IsPassword();
            
            _dbContext.Users.Add(user);
            
            var salt = PasswordGenerator.GenerateSalt();
            var pass = PasswordGenerator.HashPassword(password, salt);
            _dbContext.Credentials.Add(new HashedCredential()
            {
                UserId = user.Id,
                Salt = salt,
                HashedPassword = pass
            });
            
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task RemoveUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            _dbContext.Users.Remove(user);
            
            var cred = _dbContext.Credentials.Where(c => c.UserId == id);
            _dbContext.Credentials.RemoveRange(cred);
            
            var rates = _dbContext.SalaryRateRequests.Where(c => c.UserId == id);
            _dbContext.SalaryRateRequests.RemoveRange(rates);
            
            var srates = _dbContext.SalaryRates.Where(c => c.UserId == id);
            _dbContext.SalaryRates.RemoveRange(srates);
            
            var roles = _dbContext.Roles.Where(r => r.UserId == id);
            _dbContext.Roles.RemoveRange(roles);
            
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task ModifyUserProfile(int id, UserProfile profile)
        {
            var userCheckingNick = await _rep.GetUserByNickname(profile.NickName);
            var userCheckingEmail = await _rep.GetUserByNickname(profile.Email);
            var user = await _rep.GetUserById(id);

            if (user.NickName != profile.NickName)
                Check.Value(user?.Id).EqualsTo(userCheckingNick?.Id, ErrorMessages.UserWithNicknameExistsMsg(profile.NickName));
            if (user.Email != profile.Email)
                Check.Value(user?.Id).EqualsTo(userCheckingEmail?.Id, ErrorMessages.UserWithEmailExistsMsg(profile.Email));
            
            _dbContext.Users.Update(_mapper.Map(profile, user));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task AssignUserToManager(int userId, int managerId)
        {
            var userRecord = await _rep.GetUserById(userId);
            var managerRecord = await _rep.GetUserById(managerId);

            Check.Value(userRecord).NotNull(ErrorMessages.NoUserWithIdMsg(userId));
            Check.Value(managerRecord).NotNull(ErrorMessages.NoUserWithIdMsg(managerId));
            
            var user = await _rep.GetUserById(userId);
            
            user.ManagerId = managerId;
            _dbContext.Update(user);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
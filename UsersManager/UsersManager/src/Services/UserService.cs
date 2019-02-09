using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;
using UsersManager.Repositories;
using AutoMapper;

namespace UsersManager.Services
{
    public interface IUserService
    {
        Task ModifyUserProfile(int id, UserProfile profile);
        Task<UserProfile> GetUserProfile(int id);
        Task<User> GetUser(int id);
        Task<User> GetUser(string nickname);
        Task CreateUser(User user);
        Task AssignUserToManager(int userId, int manageId);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _rep;
        private readonly CompanyDbContext _dbContext;
        private readonly IMapper _userToProfileMapper, _profileToUserMapper;

        public UserService(IUserRepository rep, CompanyDbContext context)
        {
            _rep = rep;
            _dbContext = context;
            
            _userToProfileMapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserProfile>()).CreateMapper();
            _profileToUserMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, User>()).CreateMapper();
        }

        
        public async Task<UserProfile> GetUserProfile(int id)
        {
            var user = await _rep.GetUserById(id);
            
            return _userToProfileMapper.Map<UserProfile>(user);
        }
        
        public async Task<User> GetUser(int id)
        {
            return await _rep.GetUserById(id);;
        }
        
        public async Task<User> GetUser(string nickname)
        {
            return await _rep.GetUserByNickname(nickname);;
        }

        public async Task CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task ModifyUserProfile(int id, UserProfile profile)
        {
            var user = await _rep.GetUserById(id);
            
            _dbContext.Users.Update(_profileToUserMapper.Map<UserProfile, User>(profile, user));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task AssignUserToManager(int userId, int managerId)
        {
            var user = await _rep.GetUserById(userId);
            
            user.ManagerId = managerId;
            _dbContext.Update(user);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
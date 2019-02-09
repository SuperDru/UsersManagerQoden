using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database;
using UsersManager.Database.Models;
using UsersManager.DtoModels;

namespace UsersManager.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByNickname(string nickname);
    }
    public class UserRepository: IUserRepository
    {
        private CompanyDbContext _dbContext;
        
        public UserRepository(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users
                .Include(u => u.Department)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<User> GetUserByNickname(string nickname)
        {
            return await _dbContext.Users
                .Include(u => u.Department)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.NickName == nickname);
        }
    }
}
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
        Task<User> GetUserByEmail(string email);
        Task<Role> GetUserRole(int id);
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
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<User> GetUserByNickname(string nickname)
        {
            return await _dbContext.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.NickName == nickname);
        }
        
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Role> GetUserRole(int id)
        {
            return (await _dbContext.Roles.Include(u => u.Role).FirstOrDefaultAsync(u => u.User.Id == id)).Role;
        }
    }
}
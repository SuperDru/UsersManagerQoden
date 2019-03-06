using System;
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
        Task<User> GetUser(int id);
        Task CreateUser(UserCreationRequest userCreationRequest);
        Task AssignUserToManager(int userId, int manageId);
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
        
        public async Task<User> GetUser(int id)
        {
            var user = await _rep.GetUserById(id);
            
            Check.Value(user, "get user").NotNull(ErrorMessages.NoUserWithIdMsg(id));
            
            return user;
        }

        public async Task CreateUser(UserCreationRequest userCreationRequest)
        {
            var userToCheckNick = await _rep.GetUserByNickname(userCreationRequest.NickName);
            var userToCheckEmail = await _rep.GetUserByEmail(userCreationRequest.Email);
            Check.Value(userToCheckNick, "create user")
                .IsNull(ErrorMessages.UserWithNicknameExistsMsg(userCreationRequest.NickName));
            Check.Value(userToCheckEmail, "create user")
                .IsNull(ErrorMessages.UserWithEmailExistsMsg(userCreationRequest.Email));
            Check.Value(userCreationRequest.Password, "password").IsPassword();

            var count = await _dbContext.Users.CountAsync();
            var user = _mapper.Map<User>(userCreationRequest);
            user.Id = count + 1;
            user.Guid = Guid.NewGuid();
            _dbContext.Users.Add(user);
            
            var salt = PasswordGenerator.GenerateSalt();
            var pass = PasswordGenerator.HashPassword(userCreationRequest.Password, salt);
            _dbContext.Credentials.Add(new HashedCredential()
            {
                UserId = user.Id,
                Salt = salt,
                HashedPassword = pass
            });
            
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task ModifyUserProfile(int id, UserProfile profile)
        {
            var userToCheckNick = await _rep.GetUserByNickname(profile.NickName);
            var userToCheckEmail = await _rep.GetUserByEmail(profile.Email);
            var user = await _rep.GetUserById(id);

            if (user.NickName != profile.NickName)
                Check.Value(user?.Id).EqualsTo(userToCheckNick?.Id, ErrorMessages.UserWithNicknameExistsMsg(profile.NickName));
            if (user.Email != profile.Email)
                Check.Value(user?.Id).EqualsTo(userToCheckEmail?.Id, ErrorMessages.UserWithEmailExistsMsg(profile.Email));
            
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
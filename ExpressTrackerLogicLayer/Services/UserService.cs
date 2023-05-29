using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerDBAccessLayer.Repositories;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        public async Task<BLUser> Add(BLUser user)
        {
            user.UserId = Guid.NewGuid().ToString();
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.IsActive = true;
            user.IsPermanentDelete = false;
            var mapper = AutoMappers.InitializeUserAutoMapper();
            User _user = mapper.Map<User>(user);
            _user = await _userRepository.Add(_user);
            if (_user == null) return null;
            return user;
        }

        public Task<bool> ChangePassword(string Username, string OldPassword, string NewPassword)
        {
            return _userRepository.ChangePassword(Username, OldPassword, NewPassword);  
        }

        public Task<BLUser> Delete(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<BLUser> Get(string Username, string userPassword)
        {
            User _user = await _userRepository.Get(Username, userPassword);
            if (_user == null) return null;
            return new BLUser()
            {
                UserId = _user.UserId,
                Username = _user.Username,
                Password = ""
            };
        }

        public async Task<bool> GetById(string UserId)
        {
            return await _userRepository.GetById(UserId);
        }

        public async Task<BLUser> GetUserById(string UserId)
        {
            User user = await _userRepository.GetUserById(UserId);
            return new BLUser()
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = null
            };
        }

        public async Task<BLUser> Update(BLUser user)
        {
            var mapper = AutoMappers.InitializeUserAutoMapper();
            User _user = mapper.Map<User>(user);
            _user = await _userRepository.Update(_user);
            if (_user == null) return null;
            return user;
        }
    }
}

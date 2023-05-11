using ExpressTrackerDBAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer.Contracts
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(string UserId);
        Task<User> Get(string UserId, string userPassword);
        Task<bool> GetById(string UserId);
        Task<User> GetUserById(string UserId);
    }
}

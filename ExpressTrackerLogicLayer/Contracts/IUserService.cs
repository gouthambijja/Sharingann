using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Contracts
{
    public interface IUserService
    {
        Task<BLUser> Add(BLUser user);
        Task<BLUser> Update(BLUser user);
        Task<BLUser> Delete(string UserId);
        Task<BLUser> Get(string UserId, string userPassword);
        Task<bool> GetById(string UserId);
        Task<BLUser> GetUserById(string UserId);
        Task<bool> ChangePassword(string Username, string OldPassword, string NewPassword);
    }
}

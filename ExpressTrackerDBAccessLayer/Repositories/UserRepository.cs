using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ExpenseTrackerDBContext _UserDBContext;
        public UserRepository(ExpenseTrackerDBContext userDbContext)
        {
            _UserDBContext = userDbContext;
        }
        public async Task<User> Add(User user)
        {

            try
            {
                 _UserDBContext.Add(user);
               await _UserDBContext.SaveChangesAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> Delete(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string UserId, string userPassword)
        {
            try
            {
                var x = await _UserDBContext.Users.
                    Where(u => u.UserId == UserId && u.Password == userPassword)
                    .FirstOrDefaultAsync();
                return x;
            }
            catch
            {
                return null;
            }
        }

        public async  Task<User> GetById(string UserId)
        {
            return await _UserDBContext.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<User> Update(User _user)
        {
            var users = await _UserDBContext.Users.
                Where(u => u.UserId == _user.UserId).ToListAsync();
            var user = users[0];
            if (user == null) { return null; }
            user.Username = _user.Username;
            await _UserDBContext.SaveChangesAsync();
            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Server.Models
{
    public class User
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
    }
}

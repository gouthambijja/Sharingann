using System.Text;

namespace ExpenseTracker.Server
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            string salt = "$2a$10$xnQs0sStJoMyMhgeSiCuuO";
            return BCrypt.Net.BCrypt.HashPassword(password, "$2a$10$xnQs0sStJoMyMhgeSiCuuO");
        }
    }
}

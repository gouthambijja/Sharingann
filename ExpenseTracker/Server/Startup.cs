using ExpenseTracker.Server.Controllers;
using ExpressTrackerLogicLayer;

namespace ExpenseTracker.Server
{
    public class Startup
    {
        public static void StartUpConfigure(IServiceCollection services,ConfigurationManager configuration)
        {
            UserController.configure(configuration);
            LogicBootstrap.Configure(services, configuration);
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("sharingan", "$2a$10$xnQs0sStJoMyMhgeSiCuuO"));
            Console.WriteLine(";slad;jdff");
        }
    }
}

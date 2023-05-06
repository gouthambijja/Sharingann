using ExpressTrackerLogicLayer;

namespace ExpenseTracker.Server
{
    public class Startup
    {
        public static void StartUpConfigure(IServiceCollection services,ConfigurationManager configuration)
        {
            LogicBootstrap.Configure(services, configuration);
        }
    }
}

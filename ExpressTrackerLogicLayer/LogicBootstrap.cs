using ExpressTrackerDBAccessLayer;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer
{
    public class LogicBootstrap
    {
        public static void Configure(IServiceCollection services, ConfigurationManager config)
        {
            
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ITransactionService,TransactionService>();   
                services.AddScoped<ICategoryService, CategoryService>();
                DB_bootstrap.Configure(services, config);
        }
    }
}

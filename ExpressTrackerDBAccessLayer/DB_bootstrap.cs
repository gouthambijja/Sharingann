
using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerDBAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer
{
    public class DB_bootstrap
    {
        public static void Configure(IServiceCollection services, ConfigurationManager config)
        {
            services.AddDbContext<ExpenseTrackerDBContext>(
                            options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>(); 
        }
    }
}

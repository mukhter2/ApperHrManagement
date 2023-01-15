using HrManagement.context.efcore;
using HrManagement.interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.context
{
    public static class contextStartup
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
/*            services.AddDbContext<SocialNetworkDbContext>(options => {
                
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"), migration => migration.MigrationsAssembly("SocialNetwork.Persistence"));
            });*/

            services.AddDbContext<EF_DataContext>(o => o.UseNpgsql(configuration.GetConnectionString("Ef_Postgres_Db")));
            //services.AddHttpContextAccessor();
            

        }
    }
}

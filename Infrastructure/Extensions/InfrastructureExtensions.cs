using Domain.Repositories;
using Infrastructure.DbContextTest;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModuleDbTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NorthwindContextTest>(options => options.UseInMemoryDatabase("NorthwindTestDb"));

            return services;
        }
    }
}

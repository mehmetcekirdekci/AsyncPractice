using Application.Services;
using Application.Services.CategoryServices;
using Application.Services.ProductServices;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ApplicationModuleExtension
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureModule(configuration);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            return services;
        }

        public static IServiceCollection AddApplicationModuleTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureModuleDbTest(configuration);
            services.AddInfrastructureModule(configuration);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }


    }
}

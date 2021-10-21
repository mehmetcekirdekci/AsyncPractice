using Application.AutoMapper;
using Application.Extensions;
using Domain.Entities;
using Infrastructure.DbContextTest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Test.APITests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);

            services.AddAutoMapper(x => x.AddProfile(typeof(AutoMapperConfiguration)));

            services.AddApplicationModuleTest(Configuration);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NorthwindContextTest>();

            AddTestData(context);

            base.Configure(app, env);
        }

        private void AddTestData(NorthwindContextTest contextTest)
        {
            for (int i = 0; i < 10; i++)
            {
                Category category = new();

                category.CategoryName = $"Category {i}";
                contextTest.Categories.Add(category);
                contextTest.SaveChanges();

                for (int j = 0; j < 20; j++)
                {
                    Product product = new();
                    product.ProductName = $"Product {j}";
                    product.UnitPrice = i;
                    product.CategoryId = category.Id;

                    contextTest.Products.Add(product);
                }
                contextTest.SaveChanges();
            }
        }
    }
}

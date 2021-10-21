using Application.Models.ProductModels;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Test.APITests
{
    public class ProductsController_Test : IClassFixture<APIFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public ProductsController_Test(APIFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_Should_Return_Fail_With_Error_Response_When_Insert_ProductName_Is_Empty()
        {
            var product = new ProductDTO { ProductName = string.Empty };

            var json = JsonSerializer.Serialize(product);

            var client = _factory.CreateClient();

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/products", content);

            var actualStatusCode = response.StatusCode;

            Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        }
    }
}

using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Persistence.Exceptions.ProductExceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Add(Product product)
        {
            var sql = "INSERT INTO Products (ProductName,UnitPrice,CategoryId) VALUES(@ProductName,@UnitPrice,@CategoryId)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, product);

                if (product.ProductName == null)
                {
                    throw new ProductDontAddException(ProductExceptionsMessages.ProductDontAdd);
                }
                else
                {
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Products WHERE ProductId = @ProductId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, new { ProductId = id });
            }
        }

        public async Task<IList<Product>> GetAll()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Product>(sql);

                if (result == null)
                {
                    throw new ProductNotFoundException(ProductExceptionsMessages.ProductNotFound);
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public async Task<Product> GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE ProductId = @ProductId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { ProductId = id });

                if (result == null)
                {
                    throw new ProductNotFoundException(ProductExceptionsMessages.ProductNotFound);
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task Update(Product product)
        {
            var sql = "UPDATE Products SET ProductName = @ProductName, UnitPrice = @UnitPrice, CategoryId = @CategoryId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, product);

                if (product == null)
                {
                    throw new ProductDontUpdateException(ProductExceptionsMessages.ProductDontUpdate);
                }
                else
                {
                    return;
                }
            }
        }
    }
}

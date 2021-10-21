using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Persistence.Exceptions.CategoryExceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Add(Category category)
        {
            var sql = "INSERT INTO Categories (CategoryName) VALUES(@CategoryName)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, category);

                if (category == null)
                {
                    throw new CategoryDontAddException(CategoryExceptionsMessages.CategoryDontAdd);
                }
                else
                {
                    return;
                }
            }
        }
        public void Delete(int id)
        {
            var sql = "DELETE FROM Categories WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, new { Id = id });
            }
        }

        public async Task<IList<Category>> GetAll()
        {
            var sql = "SELECT * FROM Categories";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Category>(sql);

                if (result == null)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryNotFound);
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public async Task<Category> GetById(int id)
        {
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });

                if (result == null)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryNotFound);
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task Update(Category category)
        {
            var sql = "UPDATE Products SET CategoryName = @CategoryName";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, category);

                if (category == null)
                {
                    throw new CategoryDontUpdateException(CategoryExceptionsMessages.CategoryDontUpdate);
                }
                else
                {
                    return;
                }
            }
        }
    }
}

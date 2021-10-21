using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        void Delete(int id);
    }
}

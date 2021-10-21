using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        void Delete(int id);
    }
}

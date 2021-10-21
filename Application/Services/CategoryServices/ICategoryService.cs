using Application.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetById(int id);
        Task<List<CategoryDTO>> GetAll();
        Task Add(CategoryCreateDTO categoryCreateDTO);
        Task Update(CategoryUpdateDTO categoryCreateDTO, int id);
        bool Delete(int id);
    }
}

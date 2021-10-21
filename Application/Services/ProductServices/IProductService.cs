using Application.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductDTO> GetById(int id);
        Task<List<ProductDTO>> GetAll();
        Task Add(ProductCreateDTO productCreateDTO);
        Task Update(ProductUpdateDTO productUpdateDTO, int id);
        bool Delete(int id);
    }
}

using Application.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Exceptions.ProductExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            await _productRepository.Add(product);
        }

        public bool Delete(int id)
        {
            _productRepository.Delete(id);
            return true;
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return dtos;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            var mappedProduct = _mapper.Map<ProductDTO>(product);
            return mappedProduct;
        }

        public async Task Update(ProductUpdateDTO productUpdateDTO, int id)
        {
            var mappedProduct = _mapper.Map<Product>(productUpdateDTO);
            var updatedProduct = await _productRepository.GetById(id);

            updatedProduct.CategoryId = mappedProduct.CategoryId;
            updatedProduct.UnitPrice = mappedProduct.UnitPrice;
            updatedProduct.ProductName = mappedProduct.ProductName;
            await _productRepository.Update(updatedProduct);
        }
    }
}

using Application.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryCreateDTO categoryCreateDTO)
        {
            var category = _mapper.Map<Category>(categoryCreateDTO);
            await _categoryRepository.Add(category);
        }

        public bool Delete(int id)
        {
            _categoryRepository.Delete(id);
            return true;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            var dtos = _mapper.Map<List<CategoryDTO>>(categories);
            return dtos;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            var mappedCategory = _mapper.Map<CategoryDTO>(category);
            return mappedCategory;
        }

        public async Task Update(CategoryUpdateDTO categoryUpdateDTO, int id)
        {
            var mappedCategory = _mapper.Map<Category>(categoryUpdateDTO);
            var updatedCategory = await _categoryRepository.GetById(id);

            updatedCategory.CategoryName = mappedCategory.CategoryName;
       
            await _categoryRepository.Update(updatedCategory);
        }
    }
}

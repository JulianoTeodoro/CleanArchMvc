using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository ,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Create(category);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategorys()
        {
            var category = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(category);
        }

        public async Task Remove(int id)
        {
            var category = await _categoryRepository.GetById(id);
            await _categoryRepository.Remove(category);
        }
        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Update(category);
        }
    }
}

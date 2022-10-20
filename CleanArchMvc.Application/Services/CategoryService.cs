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
        protected readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Create(category);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            var categoryDTO = _mapper.Map<Task<CategoryDTO>>(category);
            return await categoryDTO;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategorys()
        {
            var category = _categoryRepository.GetCategoriesAsync();
            var categoryDTO = _mapper.Map<Task<IEnumerable<CategoryDTO>>>(category);
            return await categoryDTO;
        }

        public async Task Remove(int id)
        {
            var categoryDTO = _mapper.Map<CategoryDTO>(_categoryRepository.GetById(id));
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Remove(category);
        }
        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Update(category);
        }
    }
}

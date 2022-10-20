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
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        protected readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            var productDTO = _mapper.Map<Task<ProductDTO>>(product);
            return await productDTO;
        }

        public async Task<ProductDTO> GetProductCategory(int id)
        {
            var product = _productRepository.GetProductCategoryAsync(id);
            var productDTO = _mapper.Map<Task<ProductDTO>>(product);
            return await productDTO;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var product = _productRepository.GetProductsAsync();
            var productDTO = _mapper.Map<Task<IEnumerable<ProductDTO>>>(product);
            return await productDTO;
        }

        public async Task Remove(int id)
        {
            var productDTO = _mapper.Map<ProductDTO>(_productRepository.GetById(id));
            var productRemove = _mapper.Map<Product>(productDTO);
            await _productRepository.Remove(productRemove);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Update(product);
        }
    }
}

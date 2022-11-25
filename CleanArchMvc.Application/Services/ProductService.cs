using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
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
        private readonly IMediator _mediator;

        //protected readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IMediator mediator) {
            _mapper = mapper;
            _mediator = mediator;
        }

        /*public async Task Add(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
        }*/

        public async Task Add(ProductDTO product)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);

            await _mediator.Send(productCreateCommand);
        }

        /*public async Task<ProductDTO> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(product);
        }*/

        public async Task<ProductDTO> GetById(int id)
        {
            var productQueryByID = new GetProductByIdQuery(id);

            if (productQueryByID is null) throw new ApplicationException("Entity error");

            var result = await _mediator.Send(productQueryByID);

            return _mapper.Map<ProductDTO>(result);
        }

        /*public async Task<ProductDTO> GetProductCategory(int id)
        {
            var product = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }*/

        public async Task<ProductDTO> GetProductCategory(int id)
        {
            var productQueryCategory = new GetProductCategoryQuery(id);

            if (productQueryCategory is null) throw new ApplicationException("Entity error");

            var result = await _mediator.Send(productQueryCategory);

            return _mapper.Map<ProductDTO>(result);
        }

        /*public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var product = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }*/

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery is null) throw new ApplicationException("Entity error");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        /*public async Task Remove(int id)
        {
            var product = await _productRepository.GetById(id);
            await _productRepository.Remove(product);
        }*/

        public async Task Remove(int id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);

            if (productRemoveCommand is null) throw new ApplicationException("Entity error delete");

            await _mediator.Send(productRemoveCommand);
        }

        /*public async Task Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Update(product);
        }*/

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);

            await _mediator.Send(productUpdateCommand);
        }
    }
}

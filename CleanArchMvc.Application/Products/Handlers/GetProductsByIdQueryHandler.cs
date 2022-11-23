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

namespace CleanArchMvc.Application.Products.Handlers
{
    public class GetProductsByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        protected readonly IProductRepository _productRepository;

        public GetProductsByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product is null)
            {
                throw new ApplicationException("Não existe");
            }
            else
            {
                return product;
            }
        }
    }
}

using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> Create(Product product)
        {
            await _productContext.AddAsync(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetById(int id)
        {
            return await _productContext.Products.FindAsync(id);
        }

        public async Task<Product> GetProductCategoryAsync(int id)
        {
            return await _productContext.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _productContext.Entry(product).State = EntityState.Modified;
            await _productContext.SaveChangesAsync();
            return product;
        }
    }
}

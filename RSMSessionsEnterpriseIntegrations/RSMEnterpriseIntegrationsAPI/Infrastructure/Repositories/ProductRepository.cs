﻿namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly AdvWorksDbContext _context;
        public ProductRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(Product product)
        {
            await _context.AddAsync(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByID(int id)
        {
            return await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.ProductID == id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            _context.Update(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProduct(Product product)
        {
            _context.Remove(product);
            return await _context.SaveChangesAsync();
        }
    }
}

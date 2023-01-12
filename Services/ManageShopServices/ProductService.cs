using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Services.ManageShopServices
{
    public class ProductService: IProductService
    {
        private readonly SqlDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(SqlDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<bool> AddProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

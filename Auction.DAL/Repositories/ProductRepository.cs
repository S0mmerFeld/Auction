using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;
using Auction.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AuctionDbContext _auctionDbContext;

        public ProductRepository(AuctionDbContext auctionDbContext)
        {
            _auctionDbContext = auctionDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _auctionDbContext.ProductCategories.ToListAsync();
           
            return categories; 
        
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _auctionDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _auctionDbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _auctionDbContext.Products
                                     .Include(p => p.ProductCategory).ToListAsync();

            return products;
        
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await _auctionDbContext.Products
                                     .Include(p => p.ProductCategory)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }

        public async Task SaveAsync()
        {
            await _auctionDbContext.SaveChangesAsync();            
        }



        public async Task<Product> AddItem(Product product)
        {
            if (product != null)
            {
                var result = await _auctionDbContext.Products.AddAsync(product);
                await SaveAsync();
                return result.Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<Product> DeleteItem(int id)
        {
            var item = await _auctionDbContext.Products.FindAsync(id);

            if (item != null)
            {
                _auctionDbContext.Products.Remove(item);
                await _auctionDbContext.SaveChangesAsync();
            }
            return item;
        }
    }
}

using Auction.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);
        Task<IEnumerable<Product>> GetItemsByCategory(int id);
        Task SaveAsync();
        Task<Product> AddItem(Product product);
        Task<Product> DeleteItem(int id);

    }
}

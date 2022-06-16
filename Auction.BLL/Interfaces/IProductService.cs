using Auction.Models;
using Auction.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);
        Task<IEnumerable<Product>> GetItemsByCategory(int id);
    }
}

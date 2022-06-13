using Auction.Models;
using Auction.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product model);
        Task UpdateAsync(int id, Product model);
        Task DeleteAsync(int id);
    }
}

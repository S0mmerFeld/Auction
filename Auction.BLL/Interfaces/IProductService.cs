using Auction.Models;
using Auction.Models.DTO;
using Auction.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<IEnumerable<ProductCategoryDto>> GetCategories();
        Task<ProductDto> GetItem(int id);
        Task<ProductCategoryDto> GetCategory(int id);
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int id);
    }
}

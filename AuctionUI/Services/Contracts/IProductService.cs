using Auction.BLL.DTO;
using Auction.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionUI.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);

    }
}

using Auction.Models.DTO;
using Auction.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);

    }
}

using Auction.Models.DTO;
using Auction.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
        Task SaveAsync();

    }
}

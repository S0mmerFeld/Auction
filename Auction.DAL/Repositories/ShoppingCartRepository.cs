using Auction.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;

namespace Auction.DAL.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AuctionDbContext _auctionDbContext;

        public ShoppingCartRepository(AuctionDbContext auctionDbContext)
        {
            _auctionDbContext = auctionDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _auctionDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _auctionDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await _auctionDbContext.CartItems.AddAsync(item);
                    await _auctionDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await _auctionDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                _auctionDbContext.CartItems.Remove(item);
                await _auctionDbContext.SaveChangesAsync();
            }
            
            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in _auctionDbContext.Carts
                          join cartItem in _auctionDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in _auctionDbContext.Carts
                          join cartItem in _auctionDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _auctionDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await _auctionDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async Task SaveAsync()
        {
            await _auctionDbContext.SaveChangesAsync();
        }
    }
}

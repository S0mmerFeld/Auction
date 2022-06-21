using Auction.BLL.Interfaces;
using Auction.DAL;
using Auction.DAL.Repositories.Contracts;
using Auction.Models;
using Auction.Models.DTO;
using Auction.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {

            var result = await _shoppingCartRepository.AddItem(cartItemToAddDto);
            return result;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await _shoppingCartRepository.DeleteItem(id);
            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await _shoppingCartRepository.GetItem(id);
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await _shoppingCartRepository.GetItems(userId);
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _shoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);

            if (item != null)
            {
                
                return item;
            }

            return null;
        }
    }
}

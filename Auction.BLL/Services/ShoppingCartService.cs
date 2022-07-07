using Auction.BLL.Interfaces;
using Auction.DAL;
using Auction.DAL.Repositories.Contracts;
using Auction.Models;
using Auction.Models.DTO;
using Auction.Models.Entities;
using AutoMapper;
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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            var products = await _productRepository.GetItems();
            var carts = await _shoppingCartRepository.GetItems(userId);

            var DestinationDto = _mapper.Map(carts, _mapper.Map<IEnumerable<Product>, IEnumerable<CartItemDto>>(products));
            return DestinationDto;
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

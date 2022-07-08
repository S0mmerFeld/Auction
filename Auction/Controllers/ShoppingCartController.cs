using Auction.BLL;
using Auction.BLL.Interfaces;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
                                      IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<IActionResult> GetItems(int userId)
        {
            try
            {
                var cartItems = await _shoppingCartService.GetItems(userId);

                if (cartItems == null)
                {
                    return NoContent();
                }

                var products = await _productService.GetItems();
                if(products == null)
                {
                    throw new Exception("No products exist in the system");
                }

                var cartItemsDto = cartItems.ConvertToDto(products);


                return Ok(cartItemsDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartService.GetItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await _productService.GetItem(cartItem.ProductId);
                if(product == null)
                {
                    return NotFound();
                }

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await this._shoppingCartService.AddItem(cartItemToAddDto);

                if (newCartItem == null)
                {
                    return NoContent();
                }

                var product = await _productService.GetItem(newCartItem.ProductId);

                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({cartItemToAddDto.ProductId})");
                }

                var newCartItemDto = newCartItem.ConvertToDto(product);

                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await this._shoppingCartService.DeleteItem(id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await this._productService.GetItem(cartItem.ProductId);

                if (product == null)
                    return NotFound();
               
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        //TODO patch and post check return and update
        //[HttpPatch("{id:int}")]
        //public async Task<ActionResult<CartItemDto>> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        //{
        //    try
        //    {
        //        var cartItem = await this._shoppingCartService.UpdateQty(id, cartItemQtyUpdateDto);
        //        if (cartItem == null)
        //        {
        //            return NotFound();
        //        }

        //        var product = await _productService.GetItem(cartItem.ProductId);

        //        var cartItemDto = cartItem.ConvertToDto(product);

        //        return Ok(cartItemDto);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}

    }
}
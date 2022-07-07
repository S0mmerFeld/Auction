using Auction.BLL;
using Auction.BLL.DTO;
using Auction.DAL.Repositories.Contracts;
using Auction.Models;
using Auction.Models.DTO;
using Auction.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var products = await _productService.GetItems();


                if (products == null)
                {
                    return NotFound(products);
                }
                else
                {
                    return Ok(products);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItem(int id)
        {
            try
            {
                var product = await _productService.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var productCategory = await _productService.GetCategory(product.CategoryId);

                    var productDto = product.ConvertToDto(productCategory);

                    return Ok(productDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<IActionResult> GetProductCategories()
        {
            try
            {
                var productCategories = await _productService.GetCategories();

                return Ok(productCategories);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<IActionResult> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await _productService.GetItemsByCategory(categoryId);

                return Ok(products);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Product>> PostItem([FromBody] Product product)
        {
            try
            {
                var newProductItem = await this._productService.AddItem(product);

                if (newProductItem == null)
                {
                    return NoContent();
                }

                var productread = await _productService.GetItem(newProductItem.Id);

                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({product.Id})");
                }



                return CreatedAtAction(nameof(GetItem), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteItem(int id)
        {
            try
            {
                var product = await this._productService.DeleteItem(id);

                if (product == null)
                {
                    return NotFound();
                }

                var productread = await _productService.GetItem(product.Id);

                if (productread == null)
                    return NotFound();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
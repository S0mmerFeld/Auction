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
        public async Task<IActionResult>  GetItems()
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

                    return Ok(product);
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

    }
}

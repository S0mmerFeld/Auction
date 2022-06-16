using Auction.BLL.Repositories.Contracts;
using Auction.DAL;
using Auction.Models;
using Auction.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _productRepository.GetCategories();

            return categories;

        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _productRepository.GetCategory(id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _productRepository.GetItem(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _productRepository.GetItems();

            return products;

        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await _productRepository.GetItemsByCategory(id);
            return products;
        }
    }
}

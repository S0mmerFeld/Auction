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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetCategories()
        {
            var productCategories = await _productRepository.GetCategories();

            return _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

        }

        public async Task<ProductCategoryDto> GetCategory(int id)
        {
            var category = await _productRepository.GetCategory(id);
            return _mapper.Map<ProductCategoryDto>(category);
        }

        public async Task<ProductDto> GetItem(int id)
        {
            var product = await _productRepository.GetItem(id);
            var map = _mapper.Map<ProductDto>(product);
            return map;
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            var products = await _productRepository.GetItems();
            return _mapper.Map<IEnumerable<ProductDto>>(products);            

        }

        public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int id)
        {
            var products = await _productRepository.GetItemsByCategory(id);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}

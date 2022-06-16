using Auction.BLL;
using Auction.BLL.Repositories.Contracts;
using Auction.Models.Entities;
using ExpectedObjects;
using Moq;
using ShopOnline.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task ProductService_GetAll_ReturnsAllProducts()
        {
            //arrange
            var expected = (Products.ToList()).ToExpectedObject();

            var mock = new Mock<IProductRepository>();

            mock
                .Setup(f => f.GetItems())
                .ReturnsAsync(ProductEntities.AsEnumerable());
            
            var testservice = new ProductService(mock.Object);

            //actual
            var products = (await testservice.GetItems()).ToList();

            //assert
            expected.ShouldEqual(products);

        }






        private static IEnumerable<Product> Products =>
                new List<Product>()
                {
                   new Product{ Id = 22, Name = "Red Nike Trainers", Description = "Red Nike Trainers - available in most sizes", ImageURL = "/Images/Shoes/Shoes5.png", Price = 200, Qty = 100, CategoryId = 4 }
                };

        private static IEnumerable<Product> ProductEntities =>
                new List<Product>()
                {
                   new Product{ Id = 22, Name = "Red Nike Trainers", Description = "Red Nike Trainers - available in most sizes", ImageURL = "/Images/Shoes/Shoes5.png", Price = 200, Qty = 100, CategoryId = 4 }
                };

    }
}

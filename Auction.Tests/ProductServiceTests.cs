using Auction.BLL;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.Entities;
using AutoMapper;
using ExpectedObjects;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests
{
    public class ProductServiceTests
    {
        private readonly IMapper _mapper;

        [Fact]
        public async Task ProductService_GetAllProducts_ReturnsAllProducts()
        {
            //arrange
            var expected = (Products.ToList()).ToExpectedObject();

            var mock = new Mock<IProductRepository>();

            mock
                .Setup(f => f.GetItems())
                .ReturnsAsync(ProductEntities.AsEnumerable());
            
            var testservice = new ProductService(mock.Object, _mapper);

            //actual
            var products = (await testservice.GetItems()).ToList();

            //assert
            expected.ShouldEqual(products);

        }

        [Theory]
        [InlineData(1)]
        public async Task ProductService_GetProductById_ReturnProduct(int id)
        {
            //arrange
            var expected = (Products.FirstOrDefault(p=>p.Id==id)).ToExpectedObject();

            var mock = new Mock<IProductRepository>();

            mock
                .Setup(f => f.GetItem(It.IsAny<int>()))
                .ReturnsAsync(ProductEntities.FirstOrDefault(x => x.Id == id));

            var testservice = new ProductService(mock.Object, _mapper);

            //actual
            var product = await testservice.GetItem(id);

            //assert
            expected.ShouldEqual(product);
        }



        [Fact]
        public async Task ProductService_GetAllCategories_ReturnsAllCategories()
        {
            //arrange
            var expected = (ProductCategories.ToList()).ToExpectedObject();

            var mock = new Mock<IProductRepository>();

            mock
                .Setup(f => f.GetCategories())
                .ReturnsAsync(ProductCategoriesEntities.AsEnumerable());

            var testservice = new ProductService(mock.Object, _mapper);

            //actual
            var categories = (await testservice.GetCategories()).ToList();

            //assert
            expected.ShouldEqual(categories);

        }



        [Theory]
        [InlineData(1)]
        public async Task ProductService_GetCategoryById_ReturnsCategory(int id)
        {
            //arrange
            var expected = ProductCategories.FirstOrDefault(pc => pc.Id == id).ToExpectedObject();

            var mock = new Mock<IProductRepository>();

            mock
                .Setup(f => f.GetCategory(It.IsAny<int>()))
                .ReturnsAsync(ProductCategoriesEntities.FirstOrDefault(x => x.Id == id));

            var testservice = new ProductService(mock.Object, _mapper);

            //actual
            var category = await testservice.GetCategory(id);

            //assert
            expected.ShouldEqual(category);

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


        private static IEnumerable<ProductCategory> ProductCategories =>
                new List<ProductCategory>()
                {
                   new ProductCategory{  Id = 1, Name = "Beauty", IconCSS = "fas fa-spa" }
                };

        private static IEnumerable<ProductCategory> ProductCategoriesEntities =>
                new List<ProductCategory>()
                {
                   new ProductCategory{  Id = 1, Name = "Beauty", IconCSS = "fas fa-spa" }
                };


    }
}

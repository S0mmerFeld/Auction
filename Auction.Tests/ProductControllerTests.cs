using Auction.BLL;
using Auction.Controllers;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;
using Auction.Models.Entities;
using ExpectedObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using System.Net;
using Shouldly;
using System.Threading;
using System;

namespace Auction.Tests
{
    public class ProductControllerTests
    {
        
        [Fact]
        public async Task ProductController_GetAllProducts_Returns200()
        {
            
            //arrange
            var testservice = new Mock<IProductService>();

                testservice
                    .Setup(op => op.GetItems())
                    .ReturnsAsync(TodoMockData.GetTodos());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (OkObjectResult) await sut.GetItems();

            //assert
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task ProductController_GetAllProducts_Return404()
        {

            //arrange
            var testservice = new Mock<IProductService>();

            var list = new List<ProductDto>();
            list = null;

            testservice
                .Setup(op => op.GetItems())
                .ReturnsAsync(list);

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItems();

            //assert
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public async Task ProductController_GetAllProducts_Return500()
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                    .Setup(op => op.GetItems())
                    .Throws(new Exception());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = await sut.GetItems() as ObjectResult;

            //assert
            result.StatusCode.ShouldBe(500);
        }

        [Theory]
        [InlineData(22)]
        public async Task ProductController_GetProductById_Returns200(int id)
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                .Setup(op => op.GetItems())
                .ReturnsAsync(TodoMockData.GetTodos());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItem(id);

            //assert
            result.StatusCode.ShouldBe(200);
        }

        [Theory]
        [InlineData(1)]
        public async Task ProductController_GetProductById_Returns404(int id)
        {

            //arrange
            var testservice = new Mock<IProductService>();

            var list = new List<ProductDto>();
            list = null;

            testservice
                .Setup(op => op.GetItems())
                .ReturnsAsync(list);

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItem(id);

            //assert
            result.StatusCode.ShouldBe(404);
        }

        [Theory]
        [InlineData(2)]
        public async Task ProductController_GetProductById_Returns500(int id)
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                    .Setup(op => op.GetItems())
                    .Throws(new Exception());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = await sut.GetItem(id) as ObjectResult;

            //assert
            result.StatusCode.ShouldBe(500);
        }

        [Fact]
        public async Task ProductController_GetProductCategories_Returns200()
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                .Setup(op => op.GetCategories())
                .ReturnsAsync(TodoMockData.GetCategoryTodos());

            var sut = new ProductController(testservice.Object);

            //actual
            var resultEntity =await sut.GetProductCategories();
            var result = (ObjectResult)await sut.GetProductCategories();

            //assert
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task ProductController_GetProductCategories_Returns500()
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                .Setup(op => op.GetCategories())
                    .Throws(new Exception());

            var sut = new ProductController(testservice.Object);

            //actual
            var resultEntity = await sut.GetProductCategories();
            var result = (ObjectResult)await sut.GetProductCategories();

            //assert
            result.StatusCode.ShouldBe(500);
        }

        [Theory]
        [InlineData(22)]
        public async Task ProductController_GetCategoryById_Returns200(int id)
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                .Setup(op => op.GetCategories())
                .ReturnsAsync(TodoMockData.GetCategoryTodos());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItemsByCategory(id);

            //assert
            result.StatusCode.ShouldBe(200);
        }

        [Theory]
        [InlineData(22)]
        public async Task ProductController_GetCategoryById_Returns500(int id)
        {

            //arrange
            var testservice = new Mock<IProductService>();

            testservice
                .Setup(op => op.GetCategories())
                .Throws(new Exception());

            var sut = new ProductController(testservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItemsByCategory(id);

            //assert
            result.StatusCode.ShouldBe(500);
        }


    }


}

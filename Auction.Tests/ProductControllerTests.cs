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
    }


}

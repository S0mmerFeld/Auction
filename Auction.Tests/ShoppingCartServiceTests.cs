using Auction.BLL;
using Auction.BLL.Interfaces;
using Auction.Controllers;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;
using Auction.Models.Entities;
using ExpectedObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests
{
    public class ShoppingCartServiceTests
    {

        [Theory]
        [InlineData(2)]
        public async Task ShoppingCartService_GetItemsByUserId_Returns200(int userId)
        {
            //arrange
            var shoppingCartservice = new Mock<IShoppingCartService>();

            shoppingCartservice
                .Setup(op => op.GetItems(userId))
                .ReturnsAsync(TodoMockData.GetCartItemDTOTodos());

            var productservice = new Mock<IProductService>();

            productservice
                .Setup(op => op.GetItems())
                .ReturnsAsync(TodoMockData.GetTodos());


            var sut = new ShoppingCartController(shoppingCartservice.Object, productservice.Object);

            //actual
            var result = (OkObjectResult)await sut.GetItems(userId);

            //assert
            result.StatusCode.ShouldBe(200);
        }

        [Theory]
        [InlineData(2)]
        public async Task ShoppingCartService_GetItemsByUserId_Returns404(int userId)
        {
            //arrange
            var shoppingCartservice = new Mock<IShoppingCartService>();

            var listshopcart = new List<CartItemDto>();
            listshopcart = null;

            var listproduct = new List<ProductDto>();
            listproduct = null;

            shoppingCartservice
                .Setup(op => op.GetItems(userId))
                .ReturnsAsync(listshopcart);

            var productservice = new Mock<IProductService>();

            productservice
                .Setup(op => op.GetItems())
                .ReturnsAsync(listproduct);


            var sut = new ShoppingCartController(shoppingCartservice.Object, productservice.Object);

            //actual
            var result = (ObjectResult)await sut.GetItems(userId);

            //assert
            result.StatusCode.ShouldBe(404);
        }

            //[Theory]
            //[InlineData(22)]
            //public async Task ShoppingCartService_GetItemById_ReturnCartItem(int id)
            //{
            //    //arrange
            //    var expected = CartItems.FirstOrDefault(pc => pc.Id == id).ToExpectedObject();

            //    var mock = new Mock<IShoppingCartRepository>();

            //    mock
            //        .Setup(f => f.GetItem(id))
            //        .ReturnsAsync(CartItemEntities.FirstOrDefault(x => x.Id == id));

            //    var testservice = new ShoppingCartService(mock.Object);

            //    //actual
            //    var cart = await testservice.GetItem(id);
            //    //assert
            //    expected.ShouldEqual(cart);

            //}

            //[Theory]
            //[InlineData(2)]
            //public async Task ShoppingCartService_DeleteByIdAsync_DeletesEntity(int id)
            //{
            //    //arrange
            //    var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            //    shoppingcartrepository.Setup(repo => repo.GetItem(id)).ReturnsAsync(new CartItem() { Id = 2, CartId = 2, ProductId = 2, Qty = 30 }); ;
            //    shoppingcartrepository.Setup(f => f.DeleteItem(id));

            //    var testservice = new ShoppingCartService(shoppingcartrepository.Object);

            //    //act
            //    var cart = await testservice.GetItem(id);

            //    await testservice.DeleteItem(id);
            //    //assert
            //    shoppingcartrepository.Verify(x => x.DeleteItem(id), Times.Once);
            //}

            //[Fact]
            //public async Task ShoppingCartService_AddItem_AddsItem()
            //{
            //    //arrange
            //    var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            //    shoppingcartrepository.Setup(m => m.AddItem(It.IsAny<CartItemToAddDto>()));

            //    var testservice = new ShoppingCartService(shoppingcartrepository.Object);
            //    var category = new CartItemToAddDto { CartId = 3, ProductId = 4, Qty = 5 };

            //    //act
            //    await testservice.AddItem(category);

            //    //add equality comparer
            //    //assert
            //    shoppingcartrepository.Verify(x => x.AddItem(It.Is<CartItemToAddDto>(c => c.CartId == category.CartId && c.ProductId == category.ProductId && c.Qty == category.Qty)), Times.Once);
            //}

            //[Theory]
            //[InlineData(22)]
            //public async Task ShoppingCartService_UpdateItem_UpdatesItem(int qty)
            //{
            //    int userid = 2;
            //    //arrange
            //    var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            //    shoppingcartrepository.Setup(f => f.GetItems(userid))
            //        .ReturnsAsync(CartItemEntities.AsEnumerable());


            //    var testservice = new ShoppingCartService(shoppingcartrepository.Object);
            //    var category = new CartItemQtyUpdateDto { CartItemId = 2, Qty = 5 };

            //    //act
            //    await testservice.UpdateQty(qty, category);

            //    //assert
            //    shoppingcartrepository.Verify(x => x.AddItem(It.Is<CartItemToAddDto>(c => c.Qty == category.Qty && c.CartId == category.CartItemId)), Times.Once);
            //    shoppingcartrepository.Verify(x => x.SaveAsync(), Times.Once);
            //}


            //private static IEnumerable<CartItem> CartItems =>
            //        new List<CartItem>()
            //        {
            //           new CartItem{ Id = 22, CartId = 22, ProductId = 2, Qty = 30 }
            //        };

            //private static IEnumerable<CartItem> CartItemEntities =>
            //        new List<CartItem>()
            //        {
            //           new CartItem{ Id = 22, CartId = 22, ProductId = 2, Qty = 30 }
            //        };




        }
}

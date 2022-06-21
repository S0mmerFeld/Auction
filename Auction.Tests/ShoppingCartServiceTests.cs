using Auction.BLL;
using Auction.DAL.Repositories.Contracts;
using Auction.Models.DTO;
using Auction.Models.Entities;
using ExpectedObjects;
using Moq;
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
        public async Task ShoppingCartService_GetItemsByUserId_ReturnsAllCartItems(int userId)
        {
            //arrange
            var expected = (CartItems.ToList()).ToExpectedObject();

            var mock = new Mock<IShoppingCartRepository>();

            mock
                .Setup(f => f.GetItems(userId))
                .ReturnsAsync(CartItemEntities.AsEnumerable());

            var testservice = new ShoppingCartService(mock.Object);

            //actual
            var shoppingcarts = (await testservice.GetItems(userId)).ToList();

            //assert
            expected.ShouldEqual(shoppingcarts);
        }


        [Theory]
        [InlineData(22)]
        public async Task ShoppingCartService_GetItemById_ReturnCartItem(int id)
        {
            //arrange
            var expected = CartItems.FirstOrDefault(pc => pc.Id == id).ToExpectedObject();

            var mock = new Mock<IShoppingCartRepository>();

            mock
                .Setup(f => f.GetItem(id))
                .ReturnsAsync(CartItemEntities.FirstOrDefault(x => x.Id == id));
            
            var testservice = new ShoppingCartService(mock.Object);

            //actual
            var cart = await testservice.GetItem(id);
            //assert
            expected.ShouldEqual(cart);

        }

        [Theory]
        [InlineData(2)]
        public async Task ShoppingCartService_DeleteByIdAsync_DeletesEntity(int id)
        {
            //arrange
            var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            shoppingcartrepository.Setup(repo => repo.GetItem(id)).ReturnsAsync(new CartItem() { Id = 2, CartId = 2, ProductId = 2, Qty = 30 }); ;
            shoppingcartrepository.Setup(f => f.DeleteItem(id));

            var testservice = new ShoppingCartService(shoppingcartrepository.Object);

            //act
            var cart = await testservice.GetItem(id);

            await testservice.DeleteItem(id);
            //assert
            shoppingcartrepository.Verify(x => x.DeleteItem(id), Times.Once);
        }

        [Fact]
        public async Task ShoppingCartService_AddItem_AddsItem()
        {
            //arrange
            var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            shoppingcartrepository.Setup(m => m.AddItem(It.IsAny<CartItemToAddDto>()));

            var testservice = new ShoppingCartService(shoppingcartrepository.Object);
            var category = new CartItemToAddDto { CartId = 3, ProductId = 4, Qty = 5 };

            //act
            await testservice.AddItem(category);

            //add equality comparer
            //assert
            shoppingcartrepository.Verify(x => x.AddItem(It.Is<CartItemToAddDto>(c => c.CartId == category.CartId && c.ProductId == category.ProductId && c.Qty == category.Qty)), Times.Once);
        }

        [Theory]
        [InlineData(22)]
        public async Task ShoppingCartService_UpdateItem_UpdatesItem(int qty)
        {
            int userid = 2;
            //arrange
            var shoppingcartrepository = new Mock<IShoppingCartRepository>();
            shoppingcartrepository.Setup(f => f.GetItems(userid))
                .ReturnsAsync(CartItemEntities.AsEnumerable());


            var testservice = new ShoppingCartService(shoppingcartrepository.Object);
            var category = new CartItemQtyUpdateDto { CartItemId = 2, Qty = 5 };

            //act
            await testservice.UpdateQty(qty, category);

            //assert
            shoppingcartrepository.Verify(x => x.AddItem(It.Is<CartItemToAddDto>(c => c.Qty == category.Qty && c.CartId == category.CartItemId)), Times.Once);
            shoppingcartrepository.Verify(x => x.SaveAsync(), Times.Once);
        }


        private static IEnumerable<CartItem> CartItems =>
                new List<CartItem>()
                {
                   new CartItem{ Id = 22, CartId = 22, ProductId = 2, Qty = 30 }
                };

        private static IEnumerable<CartItem> CartItemEntities =>
                new List<CartItem>()
                {
                   new CartItem{ Id = 22, CartId = 22, ProductId = 2, Qty = 30 }
                };


        

    }
}

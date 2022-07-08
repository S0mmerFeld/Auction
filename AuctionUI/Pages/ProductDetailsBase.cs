using Auction.BLL.DTO;
using Auction.Models.DTO;
using AuctionUI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionUI.Pages
{
    public class ProductDetailsBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }



        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

        private List<CartItemDto> ShoppingCartItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                //  Product = await GetProductById(Id);
                Product = await ProductService.GetItem(Id);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
               var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);  
               NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                //Log Exception
            }
        }

        private async Task<ProductDto> GetProductById(int id)
        {
            //var productDtos = await ManageProductsLocalStorageService.GetCollection();

            //if(productDtos!=null)
            //{
            //    return productDtos.SingleOrDefault(p => p.Id == id);
            //}
            return null;
        }

    }
}

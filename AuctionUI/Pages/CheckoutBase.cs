using Auction.BLL.DTO;
using Auction.Models.DTO;
using AuctionUI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionUI.Pages
{
    public class CheckoutBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }


        protected string DisplayButtons { get; set; } = "block";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

                if (ShoppingCartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
                    TotalQty = ShoppingCartItems.Sum(p => p.Qty);
                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";

                }
                else
                {
                    DisplayButtons = "none";
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

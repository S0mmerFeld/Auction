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
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }      


        public IEnumerable<ProductDto> Products { get; set; }

        
        protected override async Task OnInitializedAsync()
        {
              //  await ClearLocalStorage();

                Products =  await ProductService.GetItems();

                     
        }


    }
}

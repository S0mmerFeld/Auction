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

        public string name = "Oleg";
        public string DisplayTime()
        {
            return $"Current time: {DateTime.Now.ToString("HH:mm:ss")}";
        }


        protected override async Task OnInitializedAsync()
        {
              //  await ClearLocalStorage();
                Products =  await ProductService.GetItems();
                                 
        }
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        { 
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

    }
}

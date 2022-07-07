using Auction.Models.DTO;
using Auction.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Tests
{
    public class TodoMockData
    {
        public static List<ProductDto> GetTodos()
        {
            return new List<ProductDto>{
             new ProductDto{
                 Id = 22, 
                 Name = "Red Nike Trainers", 
                 Description = "Red Nike Trainers - available in most sizes", 
                 ImageURL = "/Images/Shoes/Shoes5.png", 
                 Price = 200, 
                 Qty = 100, 
                 CategoryId = 4, 
                 CategoryName = "Fruit"
             }
         };
        }

        public static List<ProductCategoryDto> GetCategoryTodos()
        {
            return new List<ProductCategoryDto>{
             new ProductCategoryDto{
                 Id = 22,
                 Name = "Red Nike Trainers",
                 IconCSS = "123"                 
             }
         };
        }


        

        public static List<CartItem> GetCartItemTodos()
        {
            return new List<CartItem>{
             new CartItem{
                Id = 22,
                CartId = 22,
                ProductId = 2,
                Qty = 3                 
             }
         };
        }

        public static List<CartItemDto> GetCartItemDTOTodos()
        {
            return new List<CartItemDto>{
             new CartItemDto{
                Id = 22,
                CartId = 22,
                ProductId = 2,
                Qty = 3,
                Price = 3,
                ProductDescription = "descr",
                ProductImageURL = "URL",
                ProductName = "name",
                TotalPrice = 33                 
             }
         };
        }
    }
}

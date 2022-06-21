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
    }
}

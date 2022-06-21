using Auction.BLL.DTO;
using Auction.Models.DTO;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace AuctionUI.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    
    }
}

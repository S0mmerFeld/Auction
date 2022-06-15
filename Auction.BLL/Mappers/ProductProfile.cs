using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Models;
using Auction.Models.Entities;
using AutoMapper;

namespace Auction.BLL.DTO.Mappers
{
    public class CombineProductCartItem
    {
        public CartItem cartItem { get; set; }
        public Product product { get; set; }
    }

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageURL, act => act.MapFrom(src => src.ImageURL))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.Qty, act => act.MapFrom(src => src.Qty))
                .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.ProductCategory.Name)).ReverseMap();

            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();

            CreateMap<CartItem, CartItemDto>()
             .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForMember(dest => dest.ProductId, act => act.MapFrom(src => src.ProductId))
             .ForMember(dest => dest.CartId, act => act.MapFrom(src => src.CartId))
             .ForMember(dest => dest.Qty, act => act.MapFrom(src => src.Qty));             

            CreateMap<Product, CartItemDto>()             
             .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Name))
             .ForMember(dest => dest.ProductDescription, act => act.MapFrom(src => src.Description))
             .ForMember(dest => dest.ProductImageURL, act => act.MapFrom(src => src.ImageURL))
             .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))            
             .ForMember(dest => dest.TotalPrice, act => act.MapFrom(src => src.Qty * src.Price));
        }
    }
}

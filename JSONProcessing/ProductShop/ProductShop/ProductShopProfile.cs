using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile() 
        {
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();

            //05
            this.CreateMap<Product, ExportProductsInRangeDto>()
                .ForMember(d => d.ProductName,
                opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ProductPrice,
                opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.SellerName,
                opt => opt.MapFrom(s => $"{s.Seller.FirstName} {s.Seller.LastName}"));
        }
    }
}

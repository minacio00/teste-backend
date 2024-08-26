using AutoMapper;
using test_backend.Data.DTOs;
using test_backend.Models;

namespace test_backend.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
         CreateMap<Order, ReadOrderDTO>()
            .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src =>
                src.ProductOrders.Select(po => new ProductDetailDTO
                {
                    ProductId = po.ProductId,
                    Name = po.Product.Name,
                    Price = po.Product.Price ?? 0,
                    Quantity = po.Quantity
                })));

            CreateMap<CreateOrderDTO, Order>()
            .ForMember(dest => dest.ProductOrders, opt => opt.MapFrom(src => src.Products.Select(p => new ProductOrder
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            })));
    }

}
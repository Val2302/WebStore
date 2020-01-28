using System;
using System.Linq;
using AutoMapper;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.Models.Order;
using WebStore.Domain.Models.Product;

namespace WebStore.Helpers
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // <inputType, outputType>
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(nameof(EmployeeViewModel.Gender),
                    opt => opt.MapFrom(e => e.IsMan 
                        ? Gender.Man : Gender.Woman));

            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(nameof(Employee.IsMan),
                    opt => opt.MapFrom(ev => Gender.Man == ev.Gender));

            CreateMap<Product, ProductViewModel>()
                .ForMember(nameof(ProductViewModel.Brand),
                    opt => opt.MapFrom(p => p.Brand != null 
                        ? p.Brand.Name : String.Empty));
            

            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(nameof(ProductViewModel.Brand),
                    opt => opt.MapFrom(p => p.Brand != null
                        ? p.Brand.Name : String.Empty))
                .ForMember(nameof(ProductViewModel.BrandId),
                    opt => opt.MapFrom(p => p.Brand != null
                        ? p.Brand.Id : default(int?)))
                .ForMember(nameof(ProductViewModel.Section),
                    opt => opt.MapFrom(p => p.Section))
                .ForMember(nameof(ProductViewModel.SectionId),
                    opt => opt.MapFrom(p => p.Section.Id));

            CreateMap<BrandDto, BrandViewModel>()
                .ForMember(nameof(BrandViewModel.ProductsCount),
                    opt => opt.MapFrom(p => 0));

            CreateMap<SectionDto, SectionViewModel>();

            CreateMap<OrderDto, UserOrderViewModel>()
                .ForMember(nameof(UserOrderViewModel.TotalSum),
                    opt => opt.MapFrom(p => p.OrderItems.Sum(i => i.Price)));

            CreateMap<Brand, BrandDto>();

            CreateMap<Section, SectionDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<CreateOrderModel, Order>();

            CreateMap<ProductDto, Product>()
                .ForMember(nameof(Product.BrandId),
                    opt => opt.MapFrom(p => p.Brand == null ? default(int?) : p.Brand.Id))
                .ForMember(nameof(Product.Brand),
                    opt => opt.MapFrom(p => default(Brand)))
                .ForMember(nameof(Product.SectionId),
                    opt => opt.MapFrom(p => p.Section.Id));
        }
    }
}

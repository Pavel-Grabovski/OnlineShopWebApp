using AutoMapper;
using OnlineShop.BL.Domains;
using OnlineShopWebApp.ViewsModels;
using OnlineShop.Db.Entities;

namespace OnlineShop.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(x => x.ImagesPaths, opt => opt.MapFrom(u => u.Images.Select(i => i.Url)));
        CreateMap<Product, EditProductViewModel>()
            .ForMember(x => x.ImagesPaths, opt => opt.MapFrom(u => u.Images.Select(i => i.Url)));
        CreateMap<EditProductViewModel, Product>()
            .ForMember(x => x.Images, opt => opt.MapFrom(u => u.ImagesPaths.Select(x => new Image { Url = x })));
        CreateMap<(AddProductViewModel, IEnumerable<string>), Product>()
            .ForMember(x => x.Name, opt => opt.MapFrom(u => u.Item1.Name))
            .ForMember(x => x.Cost, opt => opt.MapFrom(u => u.Item1.Cost))
            .ForMember(x => x.Description, opt => opt.MapFrom(u => u.Item1.Description))
            .ForMember(x => x.Images, opt => opt.MapFrom(u => u.Item2.Select(x => new Image { Url = x })));


        //CreateMap<UserViewModel, EditUserViewModel>().ReverseMap();
        CreateMap<User, UserViewModel>();

        CreateMap<User, EditUserViewModel>().ReverseMap();


        CreateMap<CartItem, CartItemViewModel>();

        CreateMap<UserDeliveryInfo, UserDeliveryInfoViewModel>().ReverseMap();

        CreateMap<Order, OrderViewModel>();

        CreateMap<Cart, CartViewModel>();

        CreateMap<ChangePasswordViewModel, ChangePassword>();


        CreateMap<ProductEntity, Product>().ReverseMap();
        CreateMap<ImageEntity, Image>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
    }
}

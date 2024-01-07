using AutoMapper;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.BL;

public class CartsServices : ICartsServices
{
    private readonly ICartsRepository cartsRepository;
    private readonly IMapper mapper;

    public CartsServices(ICartsRepository cartsRepository, IMapper mapper)
    {
        this.cartsRepository = cartsRepository;
        this.mapper = mapper;
    }

    public async Task AddAsync(string login, Guid productId)
    {
        await cartsRepository.AddAsync(login, productId);
    }

    public async Task ClearAsync(string login)
    {
        await cartsRepository.ClearAsync(login);
    }

    public async Task DecreaseAmountAsync(string userId, Guid productId)
    {
        await cartsRepository.DecreaseAmountAsync(userId, productId);
    }

    public async Task RemoveAsync(string userId, Guid productId)
    {
        await cartsRepository.RemoveAsync(userId, productId);
    }

    public async Task<Cart> TryGetByLoginAsync(string login)
    {
        var cartEntity = await cartsRepository.TryGetByLoginAsync(login);
        var cart = mapper.Map<Cart>(cartEntity);
        return cart;
    }
}

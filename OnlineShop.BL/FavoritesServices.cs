using AutoMapper;
using OnlineShop.BL.Domains;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.BL;

public class FavoritesServices : IFavoriteServices
{
    private readonly IFavoriteRepository favoriteRepository;
    private readonly IMapper mapper;

    public FavoritesServices(IFavoriteRepository favoriteRepository, IMapper mapper)
    {
        this.favoriteRepository = favoriteRepository;
        this.mapper = mapper;
    }

    public async Task AddAsync(string login, Guid productId)
    {
        await favoriteRepository.AddAsync(login, productId);
    }

    public async Task ClearAsync(string login)
    {
        await favoriteRepository.ClearAsync(login);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(string login)
    {
        IEnumerable<ProductEntity> productsEntities = await favoriteRepository.GetAllAsync(login);
        var products = mapper.Map<IEnumerable<Product>>(productsEntities);
        return products;
    }

    public async Task RemoveAsync(string login, Guid productId)
    {
        await favoriteRepository.RemoveAsync(login, productId);
    }
}

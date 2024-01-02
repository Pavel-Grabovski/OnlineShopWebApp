using AutoMapper;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.BL;

public class ProductsServices : IProductsServices
{
	private readonly IProductsRepository productsRepository;
	private readonly IMapper mapper;

	public ProductsServices(IProductsRepository productsRepository, IMapper mapper)
	{
		this.productsRepository = productsRepository;
		this.mapper = mapper;
	}

	public async Task AddAsync(Product product)
	{
        var productDb = mapper.Map<ProductEntity>(product);
		await productsRepository.AddAsync(productDb);
    }

	public async Task DeleteAsync(Product product)
	{
		var productEntity = await productsRepository.TryGetByIdAsync(product.Id);
		if (productEntity != null)
            await productsRepository.DeleteAsync(productEntity);
    }
    public async Task DeleteAsync(Guid id)
    {
		await productsRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
	{
		var productsDb = await productsRepository.GetAllAsync();
		var products = productsDb.Select(productDB => mapper.Map<Product>(productDB));

		return products;
	}

	public async Task<Product> TryGetByIdAsync(Guid id)
	{
        var productDb = await productsRepository.TryGetByIdAsync(id);
		return mapper.Map<Product>(productDb);
	}

	public async Task UpdateAsync(Product product)
	{
        var productEntity = mapper.Map<ProductEntity>(product);
		await productsRepository.UpdateAsync(productEntity);
    }
}

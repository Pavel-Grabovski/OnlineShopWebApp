﻿using OnlineShop.Db.Entities;

namespace OnlineShop.Db.Interfaces
{
	public interface ICartsRepository
    {
        Task AddAsync(string userId, Product product);
        Task<Cart> TryGetByUserIdAsync(string userId);
        Task DecreaseAmountAsync(string userId, Product product);
        Task RemoveAsync(string userId, Product product);
        Task RemoveAsync(string userId);
        Task ClearAsync(string userId);
    }
}

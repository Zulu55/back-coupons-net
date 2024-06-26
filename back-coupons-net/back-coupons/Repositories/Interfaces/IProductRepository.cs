﻿using back_coupons.Entities;
using Microsoft.AspNetCore.Identity;

namespace back_coupons.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}

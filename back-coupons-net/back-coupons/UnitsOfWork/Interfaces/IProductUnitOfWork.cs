﻿using back_coupons.Entities;

namespace back_coupons.UnitsOfWork.Interfaces

{
    public interface IProductUnitOfWork
    {
        Task<ICollection<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}

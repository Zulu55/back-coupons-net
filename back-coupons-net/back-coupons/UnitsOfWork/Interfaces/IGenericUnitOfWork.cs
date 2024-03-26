﻿using back_coupons.Responses;

namespace back_coupons.UnitsOfWork.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<ActionResponse<IEnumerable<T>>> GetAsync();
        Task<ActionResponse<T>> GetAsync(int id);
        Task<ActionResponse<T>> AddAsync(T model);
        Task<ActionResponse<T>> UpdateAsync(T model);
        Task<ActionResponse<T>> DeleteAsync(int id);
    }
}

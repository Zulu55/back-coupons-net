﻿using back_coupons.Data;
using back_coupons.DTOs;
using back_coupons.Entities;
using back_coupons.Helpers;
using back_coupons.Repositories.Interfaces;
using back_coupons.Responses;
using Microsoft.EntityFrameworkCore;

namespace back_coupons.Repositories.Implementations
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly DataContext _dbContext;
        public CompanyRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ActionResponse<Company>> GetAsync(int id)
        {
            var row = await _dbContext.Companies
                .Include(c => c.Contacts!)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (row != null)
            {
                return new ActionResponse<Company>
                {
                    Successfully = true,
                    Result = row
                };
            }
            return new ActionResponse<Company>
            {
                Successfully = false,
                Message = "Registro no encontrado"
            };
        }

        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsyncFull()
        {
            return new ActionResponse<IEnumerable<Company>>
            {
                Successfully = true,
                Result = await _dbContext.Companies
                .Include(c => c.Contacts!)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _dbContext.Companies
                .Include(c => c.Contacts!)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Company>>
            {
                Successfully = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.CrossCutting.Interfaces;
using SpecificationPattern.Demo.Infrastructure.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Infrastructure.Repositories
{
    public class ReadAsyncRepository<TEntity> : IReadAsyncRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        public ReadAsyncRepository(ApplicationContext context)
        {
            _context = context;
        }

        protected readonly ApplicationContext _context;

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(fod => fod.Id.Equals(id));
        }

        public async Task<TEntity> GetSingleAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TEntity[]> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
        }

        public async Task<TEntity[]> GetAllAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).AsNoTracking().ToArrayAsync();
        }

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}

using SpecificationPattern.Demo.CrossCutting.Interfaces;
using System;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Infrastructure.Repositories
{
    public interface IReadAsyncRepository<TEntity>
    {
        Task<TEntity[]> GetAllAsync();

        Task<TEntity[]> GetAllAsync(ISpecification<TEntity> specification);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetSingleAsync(ISpecification<TEntity> specification);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Implementation
{
    public interface IBase<TEntity> where TEntity : class
    {
        TEntity GetById(long Id);
        TEntity Create(TEntity entity);
        TEntity Update(long Id, TEntity entity);
        void Delete(long Id);
        IQueryable<TEntity> GetAll();


        Task<TEntity> GetByIdAsync(long Id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(long Id, TEntity entity);
        void DeleteAsync(long Id);

        Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, bool asNoTracking = true);

        Task<IEnumerable<TEntity>> AddCollectionAsync(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> AddCollectionWithProxy(IEnumerable<TEntity> entities);
        Task UpdateCollectionAsync(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> UpdateCollectionWithProxy(IEnumerable<TEntity> entities);

        dynamic GetDynamicResultsBySql(string sql);
    }
}

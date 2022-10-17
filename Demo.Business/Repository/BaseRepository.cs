using Demo.Business.Data;
using Demo.Business.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Repository
{
    public class BaseRepository<TEntity> : IBase<TEntity> where TEntity : class
    {
        public readonly DataContext _dbContext;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual TEntity Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(long Id)
        {
            var entity = GetById(Id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public virtual TEntity GetById(long Id)
        {
            return _dbContext.Find<TEntity>(Id);

        }

        public virtual TEntity Update(long Id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            //_dbContext.Set<TEntity>().Attach(entity);
            //_dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async void DeleteAsync(long Id)
        {
            var entity = await GetByIdAsync(Id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long Id)
        {
            return await _dbContext.FindAsync<TEntity>(Id);

        }

        public virtual async Task<TEntity> UpdateAsync(long Id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, bool asNoTracking = true)
        {
            var databaseCount = await DbSet.CountAsync();
            if (asNoTracking)
                return new Tuple<IEnumerable<TEntity>, int>
                (
                    await DbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync(),
                    databaseCount
                );

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await DbSet.Skip(skip).Take(take).ToListAsync(),
                databaseCount
            );
        }

        public virtual async Task<IEnumerable<TEntity>> AddCollectionAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        public virtual IEnumerable<TEntity> AddCollectionWithProxy(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Add(entity);
                yield return entity;
            }
        }

        public virtual Task UpdateCollectionAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual IEnumerable<TEntity> UpdateCollectionWithProxy(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Update(entity);
                yield return entity;
            }
        }

        public dynamic GetDynamicResultsBySql(string sql)
        {
            using (var command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                var lst = new List<object>();
                bool wasOpen = true;
                try
                {
                    wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;
                    this._dbContext.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {

                        while (result.Read())
                        {
                            var newObject = new object();
                            dynamic myobject = new ExpandoObject();
                            IDictionary<string, object> myUnderlyingObject = myobject;
                            for (var i = 0; i < result.FieldCount; i++)
                            {
                                var name = result.GetName(i);
                                var val = result.IsDBNull(i) ? null : result[i];
                                myUnderlyingObject.Add(name, val);
                            }
                            lst.Add(myUnderlyingObject);
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    //this._dbContext.Database.CloseConnection();
                    if (!wasOpen) command.Connection.Close();
                }

                return lst;
            }
        }
    }
}

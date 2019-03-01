using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NPA.Repository
{
    internal sealed class SimpleRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : class
    {
        private readonly SimpleDbContext _dbContext;

        public SimpleRepository(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public TEntity GetById(TKey key)
        {
            return _dbContext.Set<TEntity>().Find(key);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        public int Count()
        {
            return _dbContext.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Count(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }
    }
}

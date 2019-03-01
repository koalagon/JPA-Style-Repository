using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public bool Exist(TKey key)
        {
            return true;
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var resultSet = _dbContext.Set<TEntity>().ToList();
            return _dbContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NPA.Repository
{
    public interface ICrudRepository<TEntity, in TKey> where TEntity : class
    {
        void Add(TEntity entity);

        TEntity GetById(TKey key);

        void Delete(TEntity entity);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}

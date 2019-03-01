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

        bool Exist(TKey key);

        bool Exist(Expression<Func<TEntity, bool>> predicate);

        int Count(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}

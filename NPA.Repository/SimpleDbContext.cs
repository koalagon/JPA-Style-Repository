using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NPA.Repository
{
    internal sealed class SimpleDbContext : DbContext
    {
        private readonly List<Type> _entities;

        public SimpleDbContext(string nameOrConnectionString, List<Type> entities) : base(nameOrConnectionString)
        {
            _entities = entities;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityMethod = modelBuilder.GetType().GetMethod("Entity");
            foreach (var entity in _entities)
            {
                entityMethod.MakeGenericMethod(entity).Invoke(modelBuilder, null);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

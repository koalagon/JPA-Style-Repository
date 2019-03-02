using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace NPA.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private static string _nameOrConnectionString;
        private static Hashtable _repositories;
        private static List<Type> _entities;
        private static List<Type> _attributes;
        private readonly SimpleDbContext _dbContext;
        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public static void UseSqlServer(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
            ScanAssembly();
        }

        private static void ScanAssembly()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Get Type having RepositoryAttribute
            _attributes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(RepositoryAttribute), false)).ToList();

            // Get the generic type arguments for ICrudRepository
            _entities = _attributes.Select(t => t.GetTypeInfo().ImplementedInterfaces
                .FirstOrDefault(s => s.IsInterface && s.GetGenericTypeDefinition() == typeof(ICrudRepository<,>))?
                .GenericTypeArguments).Where(s => s.First() != null).Select(s => s.First()).ToList();
        }

        public UnitOfWork()
        {
            if (string.IsNullOrEmpty(_nameOrConnectionString))
            {
                throw new InvalidOperationException("Database name or connection string is null.");
            }

            _dbContext = new SimpleDbContext(_nameOrConnectionString, _entities);
            _repositories = new Hashtable();

            foreach (var attribute in _attributes)
            {
                if (!_repositories.ContainsKey(attribute))
                {
                    var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget(attribute, new RepositoryInterceptor(_dbContext));
                    _repositories.Add(attribute, proxy);
                }
            }
        }

        public TRepository GetRepository<TRepository>()
        {
            var type = typeof(TRepository);

            if (!_repositories.ContainsKey(type))
            {
                throw new InvalidOperationException("The repository is not registered. Please check if you add the Repository attribute on the class.");
            }

            return (TRepository) _repositories[type];
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

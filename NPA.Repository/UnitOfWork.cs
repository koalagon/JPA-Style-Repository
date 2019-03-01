using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace NPA.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private static string _nameOrConnectionString;
        private static readonly Hashtable Repositories = new Hashtable();
        private readonly SimpleDbContext _dbContext;

        public static void UseSqlServer(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public UnitOfWork()
        {
            if (string.IsNullOrEmpty(_nameOrConnectionString))
            {
                throw new NullReferenceException("Database name or connection string is null.");
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Get Type having RepositoryAttribute
            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(RepositoryAttribute), false)).ToList();

            // Get the generic type arguments for ICrudRepository
            var genericTypeArgs = types.Select(t => t.GetTypeInfo().ImplementedInterfaces
                .FirstOrDefault(s => s.IsInterface && s.GetGenericTypeDefinition() == typeof(ICrudRepository<,>))?
                .GenericTypeArguments).ToList();

            var entities = genericTypeArgs.Where(s => s.FirstOrDefault() != null).Select(s => s.First()).ToList();

            _dbContext = new SimpleDbContext(_nameOrConnectionString, entities);

            var proxyGenerator = new ProxyGenerator();

            foreach (var type in types)
            {
                // Inteface doesn't inherit ICrudRepository. Skip the Type
                if (!type.GetTypeInfo().ImplementedInterfaces.Any(s => s.IsInterface && s.GetGenericTypeDefinition() == typeof(ICrudRepository<,>)))
                {
                    continue;
                }

                var proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(type, new RepositoryInterceptor(_dbContext));
                Repositories.Add(type, proxy);
            }
        }

        public TRepository GetRepository<TRepository>()
        {
            var type = typeof(TRepository);

            if (!Repositories.ContainsKey(type))
            {
                throw new InvalidOperationException("The repository is not registered. Please check if you add the Repository attribute on the class.");
            }

            return (TRepository) Repositories[type];
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

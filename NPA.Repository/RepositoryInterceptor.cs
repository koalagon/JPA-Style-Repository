using System;
using Castle.DynamicProxy;

namespace NPA.Repository
{
    internal sealed class RepositoryInterceptor : IInterceptor
    {
        private readonly SimpleDbContext _dbContext;

        public RepositoryInterceptor(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Intercept(IInvocation invocation)
        {
            var args = invocation.Method.DeclaringType?.GenericTypeArguments;
            if (args == null || args.Length != 2)
            {
                throw new InvalidOperationException();
            }

            var classType = typeof(SimpleRepository<,>).MakeGenericType(args[0], args[1]);

            var instance = Activator.CreateInstance(classType, _dbContext);
            var method = instance.GetType().GetMethod(invocation.Method.Name);
            var resultSet = method.Invoke(instance, invocation.Arguments);

            invocation.ReturnValue = resultSet;
        }
    }
}

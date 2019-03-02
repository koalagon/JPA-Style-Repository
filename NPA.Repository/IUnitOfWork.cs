using System;

namespace NPA.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        TRepository GetRepository<TRepository>();
    }
}

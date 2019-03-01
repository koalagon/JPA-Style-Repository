using System;

namespace NPA.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void SaveChangesAsync();
        TRepository GetRepository<TRepository>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPA.Repository;

namespace NPA.Console
{
    [Repository]
    public interface ITestRepository : ICrudRepository<Order, Guid>, IDisposable
    {
    }
}

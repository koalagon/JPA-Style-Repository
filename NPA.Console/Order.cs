using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace NPA.Console
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }
    }
}

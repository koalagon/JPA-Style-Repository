using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPA.Repository;

namespace NPA.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork.UseSqlServer("NPA.Console.SampleDbContext");

            var unitOfWork = new UnitOfWork();
            var orderRepository = unitOfWork.GetRepository<IOrderRepository>();
            var order = orderRepository.GetById(new Guid("D4611CCF-FA57-4CCF-9225-291DD6B879DF"));
            //orderRepository.Delete(order);
            orderRepository.Add(new Order
            {
                Id = Guid.NewGuid(),
                Product = "Sample",
                Address = "Tonight"
            });
            var find = orderRepository.FindBy(x => x.Product == "Amazon").ToList();
            var result = orderRepository.GetAll().ToList();
            result[0].Address = "Korea";

            unitOfWork.SaveChanges();

            System.Console.ReadKey();
        }
    }
}

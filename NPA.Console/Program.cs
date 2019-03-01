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
            if (order != null)
            {
                orderRepository.Delete(order);
            }

            orderRepository.Add(new Order
            {
                Id = Guid.NewGuid(),
                Product = "Product",
                Address = "Calgary"
            });

            var result1 = orderRepository.FindAll(x => x.Product == "Product").ToList();
            System.Console.WriteLine("*** Printing FindAll Result ***");
            foreach (var result in result1)
            {
                System.Console.WriteLine(@$"ID: {result.Id}, Product: {result.Product}, Address: {result.Address}");
            }
            System.Console.WriteLine("*** Printing GetAll Result ***");
            var result2 = orderRepository.GetAll().ToList();
            foreach (var result in result2)
            {
                System.Console.WriteLine(@$"ID: {result.Id}, Product: {result.Product}, Address: {result.Address}");
            }

            unitOfWork.SaveChanges();

            System.Console.ReadKey();
        }
    }
}

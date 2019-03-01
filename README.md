# JPA-Style-Repository
Implementing Repository interface is a tedious job. When I saw JpaRepository in Java side, I wondered if there's a similar implmentation of `JpaRepository` in .NET side. But, I couldn't find anything.

This repository library will be your life savior for the recurring repository implementation job.

# How To Use
**1. Define a interface inheriting ICrudRepository with Repository attribute**
```
[Repository]
public interface IOrderRepository : ICrudRepository<Order, Guid>
{
}
```

**2. Set database connection string**
```
UnitOfWork.UseSqlServer("{DB name or connection string}");
```

**3. That's it. No interface implementation. Just get the repository and use it**
```
var orderRepository = unitOfWork.GetRepository<IOrderRepository>();
orderRepository.Add(new Order
{
    Id = Guid.NewGuid(),
    Product = "Product",
    Address = "Calgary"
});

unitOfWork.SaveChanges();
```

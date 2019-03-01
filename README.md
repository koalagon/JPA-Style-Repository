# JPA-Style-Repository
Implementing Repository interface is a tedious job. When I saw JpaRepository in Java side, I wondered if there's a similar implmentation of `JpaRepository` in .NET side. But, I couldn't find anything.

This repository library will free you from the recurring repository implementation job.

# How To Use
**1. Define a interface inheriting ICrudRepository with Repository attribute**
```
[Repository]
public interface IOrderRepository : ICrudRepository<Order, Guid>
{
}
```


**2. Set database connection string**

If you are using OWIN project, you can put this configuration in `Startup.cs` or `Global.asax` file.
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

If you are using DI framework, you can inject `IUnitOfWork`.
```
// Autofac
builder.RegisterInstance(new UnitOfWork())
       .As<IUnitOfWork>();
       

// .NET MVC controller
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public ActionResult Index()
    {
        var orderRepository = _unitOfWork.GetRepository<IOrderRepository>();
        // Do your work
    }
}
```

Please see the NPA.Console project for the demo.

using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
   

[ApiController] 
[Route("items")] 
public class ItemsController : ControllerBase
{
    private readonly InMemItemsRepository repository;

    public ItemsController()
    {
        repository = new InMemItemsRepository();
    }

    [HttpGet] // This attribute indicates that the method handles GET requests
    public IEnumerable<Item> GetItems()
    {
        var items = repository.GetItems();
        return items;
    }
}
}





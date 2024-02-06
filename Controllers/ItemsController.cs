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

/// <summary>
/// Everytime a request is made to the controller, a new instance of the InMemItemsRepository() is created 
/// </summary>
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

    // Get/items/{id}
    //ActionResult allows us to return a httpAction result or the object
    [HttpGet("{id}")]
    public ActionResult< Item> GetItem(Guid id)
    {
        var item = repository.GetItem(id);
        if(item is null){
            return NotFound();
        }
        return item;
    }
}
}





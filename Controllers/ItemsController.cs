using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
   

[ApiController] 
[Route("items")] 
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository repository;

    /// <summary>
    /// Everytime a request is made to the controller, a new instance of the InMemItemsRepository() was being created
    /// In order to avoid opearting on a concrete instance, IItemsRepository (interface) is injected in the controller.
    /// that way ItemsController is not tightly coupled to a dependency.
    /// </summary>
    public ItemsController(IItemsRepository repository)
    {
        this.repository = repository;
    }

    // This attribute indicates that the method handles GET requests
    [HttpGet] 
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





using Catalog.Dtos;
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

    
    /// <summary>
    /// Translating the fetched item to an itemDto with the extension method AsDto()
    /// [HttpGet] attribute indicates that the method handles GET requests
    /// </summary>
    /// <returns>ItemDto</returns>
    [HttpGet] 
    public IEnumerable<ItemDto> GetItems()
    {
        var items = repository.GetItems().Select(item => item.AsDto());
        return items;
    }

    // Get/items/{id}
    /// <summary>
    /// ActionResult allows us to return a httpAction result or the object.
    /// GetItem gets the item first, checks if it is null then returns it as itemDto
    /// </summary>
    /// <param name="id"></param>
    /// <returns>itemDto</returns>
    //
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
        var item = repository.GetItem(id);
        if(item is null){
            return NotFound();
        }
        return item.AsDto();
    }

    ///<summary>
    ///Post/items
    /// ActionResult to return more than one object.
    /// The convetion for post (create methods) is to create the objects than return the object that got created
    /// and a header that specifies from where to get info about the created object
    /// </summary>
    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow

        };
        repository.CreateItem(item);
        return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());

    }
}
}





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
    private readonly ILogger<ItemsController> logger;

    /// <summary>
    /// Everytime a request is made to the controller, a new instance of the InMemItemsRepository() was being created
    /// In order to avoid opearting on a concrete instance, IItemsRepository (interface) is injected in the controller.
    /// that way ItemsController is not tightly coupled to a dependency.
    /// </summary>
    public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    
    /// <summary>
    /// Translating the fetched item to an itemDto with the extension method AsDto()
    /// [HttpGet] attribute indicates that the method handles GET requests
    /// </summary>
    /// <returns>ItemDto</returns>
    [HttpGet] 
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived{items.Count()} items");
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
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
        var item = await repository.GetItemAsync(id);
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
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow

        };
        await repository.CreateItemAsync(item);
        return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());

    }

    ///<summary>
    ///PUT/items/{id}
    /// The convetion for put is to return nothing.
    /// With-espression is specific to record types. 
    /// It allows us to create a copy of the existing object with the modified properities for the new values.
    /// Only possible during inialization since it is immutable
    /// </summary>

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
        var existingItem = await repository.GetItemAsync(id);
        if(existingItem is null)
        {
            return NotFound();
        }
        Item updatedItem = existingItem with
        {
            Name = itemDto.Name,
            Price = itemDto.Price
        };
        await repository.UpdateItemAsync(updatedItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var existingItem = await repository.GetItemAsync(id);
        if(existingItem is null)
        {
            return NotFound();
        }
        await repository.DeleteItemAsync(id);
        return NoContent();
    }
}
}





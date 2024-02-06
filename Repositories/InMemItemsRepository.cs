using Catalog.Entities;
using static Catalog.Repositories.InMemItemsRepository;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        // This is a list of items that will be used to simulate a database  
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
        };

      

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            // This is a LINQ query that will return the first item or null if no item matches the id
            return items.Where(item => item.Id == id).SingleOrDefault();
        }
    }

}

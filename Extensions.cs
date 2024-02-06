using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    //extension methods extend the definition of one type by adding a method that can be executed on that type.
    //extension methods are implemented to avoid redundancy.
    //static methods should be used for extension methods
    public static class Extensions
    {
        /// <summary>
        /// AsDto() receives an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreatedDate = item.CreatedDate
            };
        }
    }

}
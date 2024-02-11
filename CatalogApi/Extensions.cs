using CatalogApi.Dtos;
using CatalogApi.Entities;

namespace CatalogApi
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
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }

}
using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, Decimal Price, DateTimeOffset CreatedDate);
    public record CreateItemDto([Required] string Name, string Description, [Range(1, 5000)] decimal Price);
    public record UpdateItemDto([Required] string Name, string Description, [Range(1, 5000)] decimal Price);
}
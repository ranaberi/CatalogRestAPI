using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Dtos
{
    /// <summary>
    /// CreateItemDto contains only Name and Price since the Id and CreatedDate are auto-generated in the service side 
    /// </summary>
    public record CreateItemDto
    {
        [Required]
        public string Name {get; init;}
        [Required]
        [Range(1,5000)]
        public decimal Price {get; init;}
    }
}
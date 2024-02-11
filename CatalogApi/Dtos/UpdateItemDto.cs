using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Dtos
{
    public record UpdateItemDto
    {
        [Required]
        public string Name {get; init;}
        [Required]
        [Range(1,5000)]
        public decimal Price {get; init;}
    }
}
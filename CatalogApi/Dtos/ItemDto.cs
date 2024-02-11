namespace CatalogApi.Dtos
{
    /// <summary>
    /// The returned item happens to be the same as the item stored in the repository
    /// </summary>
    public record ItemDto
    {
        public Guid Id { get; init; } // init means that the property can only be set during initialization
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
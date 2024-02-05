namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; } // init means that the property can only be set during initialization
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}

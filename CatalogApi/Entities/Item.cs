namespace CatalogApi.Entities
{
    public class Item
    {
        public Guid Id { get; set; } // init means that the property can only be set during initialization
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}

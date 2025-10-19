namespace ECommerceCoza.DAL.DataContext.Entities
{
    public class Product : TimeStample
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AdditionalInformation { get; set; } = null!;
        public decimal? BasePrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = [];
        public List<WishlistItem> WishlistItems { get; set; } = [];
    }

 
}

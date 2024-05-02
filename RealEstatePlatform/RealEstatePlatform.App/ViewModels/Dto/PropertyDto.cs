namespace RealEstatePlatform.App.ViewModels.Dto
{
    public class PropertyDto
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Location { get; set; } = default!;
        public int Price { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid ListingTypeId { get; set; }
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Username { get; set; }
    }
}

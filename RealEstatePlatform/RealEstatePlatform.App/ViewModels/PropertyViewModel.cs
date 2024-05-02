using System.ComponentModel.DataAnnotations;

namespace RealEstatePlatform.App.ViewModels
{
    public class PropertyViewModel
    {
        public Guid PropertyId { get; set; }

        [Required(ErrorMessage = "The property name is required")]
        public string PropertyName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "The price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The price must be greater than 0")]
        public int Price { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid ListingTypeId { get; set; }
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Username { get; set; }

    }
}

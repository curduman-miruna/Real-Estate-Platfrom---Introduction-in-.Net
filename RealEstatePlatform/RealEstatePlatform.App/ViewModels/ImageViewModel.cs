namespace RealEstatePlatform.App.ViewModels
{
    public class ImageViewModel
    {
        public string ImageData { get; set; } = default!;
        public Guid PropertyId { get; set; }
        public Guid ImageId { get; set; }
    }
}

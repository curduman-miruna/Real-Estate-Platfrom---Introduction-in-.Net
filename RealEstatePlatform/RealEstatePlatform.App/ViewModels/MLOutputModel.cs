namespace RealEstatePlatform.App.ViewModels
{
    public class MLOutputModel
    {
       
        public float Price { get; set; }
        public float Bedrooms { get; set; }
        public float Bathrooms { get; set; }
   
        public float Sqft_living { get; set; }
        public float[] Features { get; set; }
        public float Score { get; set; }
    }
}

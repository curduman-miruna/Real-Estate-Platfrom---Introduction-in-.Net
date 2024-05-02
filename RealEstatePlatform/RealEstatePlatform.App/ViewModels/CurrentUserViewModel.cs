namespace RealEstatePlatform.API.Models
{
    public class CurrentUserViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}

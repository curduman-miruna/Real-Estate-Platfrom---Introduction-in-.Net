using System.ComponentModel.DataAnnotations;

namespace RealEstatePlatform.App.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$",
    ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one digit, and one special character (!@#$%^&*).")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}

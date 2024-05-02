using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePlatform.App.ViewModels
{
    public class UpdateInformationViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? UniqueName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? FullName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Term5_RPBDIS_Web.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        public bool? Rights { get; set; }
    }
}

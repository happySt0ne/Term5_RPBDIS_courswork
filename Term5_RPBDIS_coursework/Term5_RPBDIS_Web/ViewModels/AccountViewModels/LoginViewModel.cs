using System.ComponentModel.DataAnnotations;

namespace Term5_RPBDIS_Web.ViewModels.AccountViewModels {
    public class LoginViewModel {
        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}

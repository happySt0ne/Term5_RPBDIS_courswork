using System.ComponentModel.DataAnnotations;

namespace Term5_RPBDIS_Web.ViewModels.AccountViewModels {
    public class UpdateAccountViewModel {
        [Required]
        public string? OldPhoneNumber { get; set; }

        public string? NewPhoneNumber { get; set; }

        public bool? NewRights { get; set; }
    }
}

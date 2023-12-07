using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Term5_RPBDIS_Web.ViewModels.AccountViewModels {
    public class DeleteAccountViewModel {
        [Required]
        public string PhoneNumber { get; set; }
    }
}

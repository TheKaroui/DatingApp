using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models {
    public class UserToRegisterModel {
        
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength (8, MinimumLength = 4, ErrorMessage = "You must specify pwd between 4 and 8 characters")]
        public string Password { get; set; }
    }
}
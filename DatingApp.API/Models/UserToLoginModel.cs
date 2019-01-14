using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models {
    public class UserToLoginModel {
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
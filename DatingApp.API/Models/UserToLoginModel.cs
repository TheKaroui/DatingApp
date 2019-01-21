using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models {
    public class UserToLoginModel {
        
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MinLength(2)]
        public string Password { get; set; }
    }
}
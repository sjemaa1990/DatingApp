using System.ComponentModel.DataAnnotations;

namespace SGS.eCalc.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage = "You must specify password between 4 and 8.")]
        public string Password { get; set; }
 
    }
}
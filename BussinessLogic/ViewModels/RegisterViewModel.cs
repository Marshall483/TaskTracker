using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Seret password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwoed confirm")]
        public string PasswordConfirm { get; set; }
    }
}

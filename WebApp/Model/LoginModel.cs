using System.ComponentModel.DataAnnotations;

namespace TodoListApp.WebApp.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email or username is required")]
        [StringLength(100, ErrorMessage = "Email or username must be less than 100 characters")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}

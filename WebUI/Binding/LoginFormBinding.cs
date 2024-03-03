using System.ComponentModel.DataAnnotations;

namespace WebUI.Binding
{
    public class LoginFormBinding
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

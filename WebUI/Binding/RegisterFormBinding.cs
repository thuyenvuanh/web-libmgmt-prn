using System.ComponentModel.DataAnnotations;

namespace WebUI.Binding;

public class RegisterFormBinding
{
    [EmailAddress, Required]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]

    [DataType(DataType.Password), Compare(nameof(Password))]
    public string? RePassword { get; set; }
}

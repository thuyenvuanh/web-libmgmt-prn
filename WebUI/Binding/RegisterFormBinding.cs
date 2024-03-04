using BusinessObjects;
using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Binding;

public class RegisterFormBinding
{

    public int Id { get; set; }

    [EmailAddress, Required]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password), MinLength(6)]
    public string? Password { get; set; }
    [Required]

    [DataType(DataType.Password), Compare(nameof(Password)), MinLength(6)]
    public string? RePassword { get; set; }

    [EnumDataType(typeof(ERole))]
    public ERole Role { get; set; } = ERole.Customer;

    public static RegisterFormBinding FromAccount(Account account)
    {
        return new()
        {
            Id = account.Id,
            Email = account.Email,
            Password = account.Password,
            RePassword = string.Empty,
            Role = (ERole)account.Role
        };
    }

    public Account ToAccount() => new() { Id = Id, Email = Email!, Password = Password!, Role = (int)Role };
}

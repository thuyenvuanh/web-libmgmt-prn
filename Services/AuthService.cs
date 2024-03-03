using BusinessObjects;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services;

public class AuthService: IAuthService
{
    private readonly IAccountRepository accountRepository;

    public AuthService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public Account? Login(string email, string password)
    {
        return accountRepository.Login(email, password);
    }

    public Account Regiser(string email, string password)
    {
        var newCustomer = new Account { Email = email, Password = password, Role = (int) ERole.Customer };
        return accountRepository.Register(newCustomer);
    }
}

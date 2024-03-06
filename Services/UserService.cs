using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services;

public class UserService : IUserService
{
    private readonly IAccountRepository accountRepository;

    public UserService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public bool DeleteAccount(Account account)
    {
        return accountRepository.Delete(account);
    }

    public Account? GetAccount(int id)
    {
        return accountRepository.GetByID(id);
    }

    public Account? GetAccountByEmail(string email)
    {
        return accountRepository.GetByEmail(email);
    }

    public List<Account> GetAccounts()
    {
        return accountRepository.GetAll();
    }

    public Account? SaveAccount(Account account)
    {
        return accountRepository.SaveAccount(account);
    }
}

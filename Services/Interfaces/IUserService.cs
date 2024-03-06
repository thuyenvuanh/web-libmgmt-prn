using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IUserService
{
    Account? GetAccount(int id);
    Account? GetAccountByEmail(string email);
    List<Account> GetAccounts();
    Account? SaveAccount(Account account);
    bool DeleteAccount(Account account);
}

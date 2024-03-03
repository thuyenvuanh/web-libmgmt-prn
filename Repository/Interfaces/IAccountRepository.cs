using BusinessObjects;
using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IAccountRepository
{
    List<Account> GetAll();
    List<Account> GetAllByRole(ERole role);
    Account? GetByEmail(string email);
    Account? GetByID(int id);
    Account SaveAccount(Account account);
    Account? Login(string email, string password);
    Account Register(Account account);
    bool Delete(Account account);
    List<Account> SearchByEmail(string email);
}

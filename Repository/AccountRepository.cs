using BusinessObjects.Models;
using BusinessObjects;
using DataAccessObject;
using Repositories.Interfaces;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
    public AccountRepository() { }

    public bool Delete(Account account)
    {
        if (account != null && account.Id > 0)
        {
            AccountDAO.Instance.Delete(account);
            return true;
        }
        return false;
    }

    public List<Account> GetAll() => AccountDAO.Instance.Get()
                                                        .ToList();

    public List<Account> GetAllByRole(ERole role) => AccountDAO.Instance.Get()
                                                                        .Where(a => a.Role == (int)role)
                                                                        .ToList();

    public Account? GetByEmail(string email) => AccountDAO.Instance.Get()
                                                                   .Where(a => a.Email == email)
                                                                   .FirstOrDefault();

    public Account? GetByID(int id) => AccountDAO.Instance.Get()
                                                          .Where(a => a.Id == id)
                                                          .FirstOrDefault();

    public Account? Login(string email, string password)
    {
        int result = 0;
        return AccountDAO.Instance.Get()
                                  .Where(a => (a.Email == email || (int.TryParse(email, out result) && a.Id == result)) && a.Password == password)
                                  .FirstOrDefault();
    }

    public Account Register(Account account)
    {
        account.Role = (int) ERole.Customer;
        AccountDAO.Instance.SaveAccount(account);
        return GetByEmail(account.Email)!;
    }

    public Account SaveAccount(Account account)
    {
        return AccountDAO.Instance.SaveAccount(account);
    }

    public List<Account> SearchByEmail(string email)
    {
        return AccountDAO.Instance.SearchByEmail(email);
    }
}
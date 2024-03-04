using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject;

public class AccountDAO
{
    private readonly LibMgmtContext _dbContext = new();

    private static AccountDAO _instance;

    public static AccountDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AccountDAO();
            }
            return _instance;
        }
    }

    private AccountDAO() { }

    public DbSet<Account> Get() => _dbContext.Accounts;

    public Account SaveAccount(Account account)
    {
        if (account.Id == 0)
        {
            _dbContext.Accounts.Add(account);
        }
        else
        {
            try
            {
                _dbContext.Accounts.Update(account);
            }
            catch (Exception)
            {
                if (_dbContext.Accounts.Any(x => x.Id == account.Id))
                {
                    _dbContext.Accounts.Attach(account).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                } else
                {
                    throw;
                }
            }
        }
        _dbContext.SaveChanges();
        return _dbContext.Accounts.Where(a => a.Email == account.Email).First();
    }

    public void Delete(Account account)
    {
        _dbContext.Accounts.Remove(account);
        _dbContext.SaveChanges();
    }

    public List<Account> SearchByEmail(string keyword) => _dbContext.Accounts.AsNoTracking().Where(a => a.Email.Contains(keyword)).ToList();
}

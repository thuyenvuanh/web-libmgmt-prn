using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject;

public class BorrowItemDAO
{
    private readonly LibMgmtContext _dbContext = new();

    private static BorrowItemDAO _instance;

    public static BorrowItemDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BorrowItemDAO();
            }
            return _instance;
        }
    }

    private BorrowItemDAO() { }

    public DbSet<BorrowItem> Get() => _dbContext.BorrowItems;

    public BorrowItem UpdateBorrowItem(BorrowItem borrowItem)
    {
        if (borrowItem.Id == 0)
        {
            _dbContext.BorrowItems.Add(borrowItem);
        }
        else
        {
            _dbContext.BorrowItems.Update(borrowItem);
        }
        _dbContext.SaveChanges();
        return borrowItem;
    }

    public List<BorrowItem> GetBooksBorrowedByAccount(Account account, bool includeReturnedBooks = false)
    {
        return _dbContext.BorrowItems
            .Include(bi => bi.BookNavigation)
            .Where(bi => bi.Borrower.Equals(account.Id) && includeReturnedBooks || (bi.ReturnedDate == null))
            .ToList();
    }

    public BorrowItem? GetBorrowItemById(int id) => _dbContext.BorrowItems.FirstOrDefault(bi => bi.Id == id);

    public BorrowItem? GetBorrowItemByAccountAndBook(int accountId, int book) => _dbContext.BorrowItems.FirstOrDefault(bi => bi.Book == book && bi.Borrower == accountId);
}

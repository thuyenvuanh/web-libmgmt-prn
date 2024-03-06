using BusinessObjects.Models;
using DataAccessObject;
using Repositories.Interfaces;

namespace Repositories;

public class BorrowItemRepository : IBorrowItemRepository
{
    public BorrowItem BorrowBook(BorrowItem borrowItem) => BorrowItemDAO.Instance.UpdateBorrowItem(borrowItem);

    public BorrowItem? GetBorrowItem(int id)
    {
        return BorrowItemDAO.Instance.GetBorrowItemById(id);
    }

    public List<BorrowItem> GetBorrowItemByAccountAndBook(int accountId, int bookId)
    {
        return BorrowItemDAO.Instance.GetBorrowItemByAccountAndBook(accountId, bookId);
    }

    public List<BorrowItem> GetBorrowItemsByAccount(Account account, bool includeReturnedBooks = false) => BorrowItemDAO.Instance.GetBooksBorrowedByAccount(account, includeReturnedBooks);

    public BorrowItem ReturnBook(BorrowItem borrowItem) => BorrowItemDAO.Instance.UpdateBorrowItem(borrowItem);
}

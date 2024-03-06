using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IBorrowItemService
{
    List<BorrowItem> GetBorrowItemsByAccount(Account account, bool includedReturnedBook = false);
    List<BorrowItem> GetBorrowItemByAccountIdAndBookId(int accountId, int bookId);
    BorrowItem ReturnBook(int id);
    BorrowItem BorrowBook(Account account, Book book);
}

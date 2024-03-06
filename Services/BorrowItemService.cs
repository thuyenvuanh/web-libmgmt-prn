using BusinessObjects.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services;

public class BorrowItemService : IBorrowItemService
{
    private readonly IBorrowItemRepository borrowItemRepository;

    public BorrowItemService(IBorrowItemRepository borrowItemRepository)
    {
        this.borrowItemRepository = borrowItemRepository;
    }

    public BorrowItem BorrowBook(Account account, Book book)
    {
        BorrowItem borrowItem = new()
        {
            Borrower = account.Id,
            Book = book.Id,
            BorrowedDate = DateTime.Now,
            Period = 2,
        };
        return borrowItemRepository.BorrowBook(borrowItem);
    }

    public List<BorrowItem> GetBorrowItemByAccountIdAndBookId(int accountId, int bookId)
    {
        return borrowItemRepository.GetBorrowItemByAccountAndBook(accountId, bookId);
    }

    public List<BorrowItem> GetBorrowItemsByAccount(Account account, bool includedReturnedBook = false)
    {
        return borrowItemRepository.GetBorrowItemsByAccount(account, includedReturnedBook);
    }

    public BorrowItem ReturnBook(int id)
    {
        BorrowItem borrowItem = borrowItemRepository.GetBorrowItem(id)!;
        if (borrowItem.ReturnedDate != null)
        {
            throw new Exception("You've already returned this book");
        }
        borrowItem.ReturnedDate = DateTime.Now;
        return borrowItemRepository.ReturnBook(borrowItem);
    }
}

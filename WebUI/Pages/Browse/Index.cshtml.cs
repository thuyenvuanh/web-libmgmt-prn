using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using WebUI.Binding;
using WebUI.Utils;

namespace WebUI.Pages.Browse
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Book> Books { get; set; }

        private readonly IBookService bookService;
        private readonly IBorrowItemService borrowItemService;

        public IndexModel(IBookService bookService, IBorrowItemService borrowItemService)
        {
            this.bookService = bookService;
            this.borrowItemService = borrowItemService;
        }

        public void OnGet(ToastMessage? toastMessage)
        {
            Books = bookService.GetAllBooks();
            if (toastMessage != null && !string.IsNullOrEmpty(toastMessage.Title) && !string.IsNullOrEmpty(toastMessage.Message))
            {
                ViewData["ToastMessage"] = new List<ToastMessage>() { toastMessage };
            }
        }

        public IActionResult OnPostBorrow(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }
            Account? currentUser = HttpContext.Session.GetObjectFromJson<Account>(SessionUtils.LOGGED_IN_USER_KEY);
            if (currentUser == null)
            {
                return Forbid();
            }
            Book? book = bookService.GetBook((int)id);
            if (book == null)
            {
                return RedirectToPage("Errors/404");
            }

            var borrowedItems = borrowItemService.GetBorrowItemByAccountIdAndBookId(currentUser.Id, book.Id).Where(b => b.ReturnedDate == null).FirstOrDefault();
            if (borrowedItems != null && borrowedItems.ReturnedDate == null)
            {
                ToastMessage errorMessage = new()
                {
                    Title = "Error",
                    Message = $"You've already borrowed this book",
                    ToastType = ToastType.Error
                };

                return RedirectToAction("Index", errorMessage);
            }

            borrowItemService.BorrowBook(currentUser, book);

            ToastMessage toastMessage = new()
            {
                Title = "Succes",
                Message = $"You borrowed {book.Name}",
                ToastType = ToastType.Success
            };

            return RedirectToAction("Index", toastMessage);
        }
    }
}

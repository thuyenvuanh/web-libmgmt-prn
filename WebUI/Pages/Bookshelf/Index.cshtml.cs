using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using WebUI.Binding;
using WebUI.Utils;

namespace WebUI.Pages.Bookshelf
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<BorrowItem> BorrowItems { get; set; }

        private readonly IBorrowItemService borrowItemService;

        public IndexModel(IBorrowItemService borrowItemService)
        {
            this.borrowItemService = borrowItemService;
        }

        public void OnGet(ToastMessage? toast)
        {
            BorrowItems = borrowItemService.GetBorrowItemsByAccount(GetCurrentUser()!, true);
            if (toast != null && !string.IsNullOrEmpty(toast.Title) && !string.IsNullOrEmpty(toast.Message))
            {
                ViewData["ToastMessage"] = new List<ToastMessage>() { toast };
            }
        }

        public IActionResult OnPostReturn(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }

            try
            {
                var returnedBook = borrowItemService.ReturnBook((int)id);
                ToastMessage toast = new()
                {
                    ToastType = ToastType.Success,
                    Title = "Success",
                    Message = $"Return {returnedBook.BookNavigation.Name}"
                };
                return RedirectToAction("Index", toast);
            }
            catch (Exception e)
            {
                ToastMessage error = new()
                {
                    ToastType = ToastType.Error,
                    Title = "Error",
                    Message = e.Message
                };
                return RedirectToAction("Index", error);
            }

        }

        private Account? GetCurrentUser()
        {
            return HttpContext.Session.GetObjectFromJson<Account>(SessionUtils.LOGGED_IN_USER_KEY);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace WebUI.Pages.Books
{
    public class DeleteModel : PageModel
    {
        public readonly IBookService bookService;

        public DeleteModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }
            var book = bookService.GetBook((int)id);

            if (book != null)
            {
                bookService.DeleteBook(book);
            }

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Services.Interfaces;

namespace WebUI.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService bookService;

        public IndexModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IList<Book> Book { get;set; } = default!;

        public void OnGet()
        {
            Book = bookService.GetAllBooks();
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookRepository bookRepository;

        public IndexModel(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IList<Book> Book { get;set; } = default!;

        public void OnGet()
        {
            Book = bookRepository.GetAll();
        }
    }
}

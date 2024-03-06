using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using WebUI.Binding;

namespace WebUI.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public EditModel(IBookService bookService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
        }

        [BindProperty]
        public NewBookBinding Book { get; set; } = default!;

        [BindProperty]
        public List<string> SelectableAuthor { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }

            var book = bookService.GetBook((int)id);
            if (book == null)
            {
                return RedirectToPage("Errors/404");
            }
            Book = NewBookBinding.FromBook(book);
            SelectableAuthor = authorService.GetAll().ConvertAll(author => $"{author.Id}. {author.Name}").ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentBook = bookService.GetBook(Book.Id);
            if (currentBook == null)
            {
                return RedirectToPage("Errors/404");
            }

            try
            {
                currentBook.UpdateWith(Book.ToBook());
                bookService.SaveBook(currentBook);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return RedirectToPage("Errors/404");
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return bookService.GetBook(id) != null;
        }
    }
}

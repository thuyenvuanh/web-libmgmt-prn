using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using WebUI.Binding;

namespace WebUI.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public CreateModel(IAuthorService authorService, IBookService bookService)
        {
            this.authorService = authorService;
            this.bookService = bookService;
        }

        [BindProperty]
        public List<String> SelectableAuthor { get; set; }

        public IActionResult OnGet()
        {
            SelectableAuthor = authorService.GetAll().ConvertAll(author => $"{author.Id}. {author.Name}").ToList();
            return Page();
        }

        [BindProperty]
        public NewBookBinding Book { get; set; } = default!;


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bookService.SaveBook(Book.ToBook());

            return RedirectToPage("Index");
        }
    }
}

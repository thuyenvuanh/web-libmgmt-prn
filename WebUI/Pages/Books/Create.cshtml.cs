using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Interfaces;
using WebUI.Binding;

namespace WebUI.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public CreateModel(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }

        [BindProperty]
        public List<String> SelectableAuthor { get; set; }

        public IActionResult OnGet()
        {
            SelectableAuthor = authorRepository.GetAll().ConvertAll(author => $"{author.Id}. {author.Name}").ToList();
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

            bookRepository.Save(Book.ToBook());

            return RedirectToPage("Index");
        }
    }
}

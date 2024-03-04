using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public CreateModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Author == null)
            {
                return Page();
            }

            authorRepository.SaveAuthor(Author);

            return RedirectToPage("./Index");
        }
    }
}

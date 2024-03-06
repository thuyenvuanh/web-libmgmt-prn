using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Services.Interfaces;

namespace WebUI.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAuthorService authorService;

        public EditModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Errors/404");
            }

            var author = authorService.GetAuthor((int)id);
            if (author == null)
            {
                return RedirectToPage("Errors/404");
            }
            Author = author;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var selectedAuthor = authorService.GetAuthor(Author.Id);

            if (selectedAuthor == null)
            {
                return RedirectToPage("Errors/404");
            }

            try
            {
                selectedAuthor.UpdateWith(Author);
                authorService.SaveAuthor(selectedAuthor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.Id))
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

        private bool AuthorExists(int id)
        {
          return authorService.GetAuthor(id) != null;
        }
    }
}

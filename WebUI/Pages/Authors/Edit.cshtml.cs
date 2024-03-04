using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace WebUI.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAuthorRepository authorRepository;

        public EditModel(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = authorRepository.GetById((int)id);
            if (author == null)
            {
                return NotFound();
            }
            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var selectedAuthor = authorRepository.GetById(Author.Id);

            if (selectedAuthor == null)
            {
                return NotFound();
            }

            try
            {
                selectedAuthor.UpdateWith(Author);
                authorRepository.SaveAuthor(selectedAuthor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.Id))
                {
                    return NotFound();
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
          return authorRepository.GetById(id) != null;
        }
    }
}

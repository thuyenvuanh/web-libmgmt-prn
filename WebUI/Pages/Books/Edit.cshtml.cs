using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Repositories;
using WebUI.Binding;

namespace WebUI.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public EditModel(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        [BindProperty]
        public NewBookBinding Book { get; set; } = default!;

        [BindProperty]
        public List<string> SelectableAuthor { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepository.GetById((int)id);
            if (book == null)
            {
                return NotFound();
            }
            Book = NewBookBinding.FromBook(book);
            SelectableAuthor = authorRepository.GetAll().ConvertAll(author => $"{author.Id}. {author.Name}").ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentBook = bookRepository.GetById(Book.Id);
            if (currentBook == null)
            {
                return NotFound();
            }

            try
            {
                currentBook.UpdateWith(Book.ToBook());
                bookRepository.Save(currentBook);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
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

        private bool BookExists(int id)
        {
            return bookRepository.GetById(id) != null;
        }
    }
}

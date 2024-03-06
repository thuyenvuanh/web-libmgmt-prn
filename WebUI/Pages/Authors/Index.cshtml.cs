using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Services.Interfaces;

namespace WebUI.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorService authorService;

        public IndexModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IList<Author> Author { get;set; } = default!;

        public void OnGet()
        {
            Author = authorService.GetAll();
        }
    }
}

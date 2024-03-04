using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Book
    {
        public Book()
        {
            BorrowItems = new HashSet<BorrowItem>();
        }

        [Display(Name = "Book ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required, Display(Name = "ISBN Number")]
        public string Isbn { get; set; } = null!;
        [Required, DataType(DataType.Date), Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }
        public int Author { get; set; }

        public virtual Author AuthorNavigation { get; set; } = null!;
        public virtual ICollection<BorrowItem> BorrowItems { get; set; }

        public void UpdateWith(Book book)
        {
            this.Id = book.Id;
            this.Name = book.Name;
            this.Isbn = book.Isbn;
            this.PublishedDate = book.PublishedDate;
            this.Author = book.Author;
        }
    }
}

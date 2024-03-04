using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        public string Name { get; set; } = null!;
        [Required, DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public void UpdateWith(Author author)
        {
            Id = author.Id;
            Name = author.Name;
            Birthday = author.Birthday;
        }
    }
}

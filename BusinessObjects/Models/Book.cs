namespace BusinessObjects.Models
{
    public partial class Book
    {
        public Book()
        {
            BorrowItems = new HashSet<BorrowItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public int Author { get; set; }

        public virtual Author AuthorNavigation { get; set; } = null!;
        public virtual ICollection<BorrowItem> BorrowItems { get; set; }
    }
}

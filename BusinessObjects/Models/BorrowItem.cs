namespace BusinessObjects.Models
{
    public partial class BorrowItem
    {
        public int Id { get; set; }
        public int Book { get; set; }
        public int Borrower { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int Period { get; set; }

        public virtual Book BookNavigation { get; set; } = null!;
        public virtual Account BorrowerNavigation { get; set; } = null!;
    }
}

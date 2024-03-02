namespace BusinessObjects.Models
{
    public partial class Account
    {
        public Account()
        {
            BorrowItems = new HashSet<BorrowItem>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }

        public virtual ICollection<BorrowItem> BorrowItems { get; set; }
    }
}

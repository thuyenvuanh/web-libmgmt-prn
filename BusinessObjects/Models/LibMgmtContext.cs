using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class LibMgmtContext : DbContext
    {
        public LibMgmtContext()
        {
        }

        public LibMgmtContext(DbContextOptions<LibMgmtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BorrowItem> BorrowItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("LibMgmt");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__Author__3B75D760");
            });

            modelBuilder.Entity<BorrowItem>(entity =>
            {
                entity.ToTable("BorrowItem");

                entity.Property(e => e.BorrowedDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.BorrowItems)
                    .HasForeignKey(d => d.Book)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BorrowItem__Book__3E52440B");

                entity.HasOne(d => d.BorrowerNavigation)
                    .WithMany(p => p.BorrowItems)
                    .HasForeignKey(d => d.Borrower)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BorrowIte__Borro__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

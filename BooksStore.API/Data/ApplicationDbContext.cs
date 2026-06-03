using BooksStore.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure the many-to-many relationship between Book and Author
            modelBuilder.Entity<Book_Author>()
                .HasKey(ba => ba.Id); // Primary key for the join table
            
            modelBuilder.Entity<Book_Author>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Book_Authors)
                .HasForeignKey(ba => ba.BookId);
            
            modelBuilder.Entity<Book_Author>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.Book_Authors)
                .HasForeignKey(ba => ba.AuthorId);
        }


        // Create Books table
        public DbSet<Book> Books => Set<Book>();

        // Create Authors table
        public DbSet<Author> Authors => Set<Author>();

        // Create Books_Authors table
        public DbSet<Book_Author> Books_Authors => Set<Book_Author>();

        // Create Publishers table
        public DbSet<Publisher> Publishers => Set<Publisher>();
    }
}

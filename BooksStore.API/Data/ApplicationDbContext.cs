using BooksStore.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Create Books table
        public DbSet<Book> Books => Set<Book>();
    }
}

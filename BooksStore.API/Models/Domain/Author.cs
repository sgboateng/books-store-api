namespace BooksStore.API.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        // Navigation property for related Books
        public ICollection<Book_Author> Book_Authors { get; set; }
    }
}

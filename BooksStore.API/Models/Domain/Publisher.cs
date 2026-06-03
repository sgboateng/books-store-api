namespace BooksStore.API.Models.Domain
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation property for related Books
        public ICollection<Book> Books { get; set; }
    }
}

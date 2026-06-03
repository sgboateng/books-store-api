namespace BooksStore.API.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class AuthorBooksDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public List<string> BookTitles { get; set; }
    }
}

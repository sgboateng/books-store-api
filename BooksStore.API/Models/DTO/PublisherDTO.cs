namespace BooksStore.API.Models.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class PublisherAuthorsBooksDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BooksAuthorsDTO> BooksAuthors { get; set; }

    }

    public class BooksAuthorsDTO
    {

        public string BookName { get; set; }

        public List<string> BooksAuthors { get; set; }
    }
}

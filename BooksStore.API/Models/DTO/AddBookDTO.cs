using System.Security.Cryptography.X509Certificates;

namespace BooksStore.API.Models.DTO
{
    public class AddBookDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public int? Rating { get; set; }

        public string Genre { get; set; }

        public string CoverUrl { get; set; }

        public int PublisherId { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}

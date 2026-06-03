using BooksStore.API.Models.Domain;

namespace BooksStore.API.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (context != null && !context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "SQL Practice Problems",
                            Description = "SQL challenges for you to solve",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-5),
                            Rating = 4,
                            Genre = "SQL",
                            // Author = "Sylvia Moestl Vasilik",
                            CoverUrl = "https://www.sqlpracticeproblems.com/",
                            DateAdded = DateTime.Now.AddDays(-10),
                            PublisherId = 1
                        },
                        new Book()
                        {
                            Title = "Designing Data-Intensive Applications",
                            Description = "The Big Ideas Behind Reliable, Scalable, and Maintainable Systems",
                            IsRead = false,
                            Genre = "Data Engineering",
                            // Author = "Martin Kleppmann",
                            CoverUrl = "https://www.oreilly.com/library/view/designing-data-intensive-applications/9781491903063/",
                            DateAdded = DateTime.Now.AddDays(-8),
                            PublisherId = 2
                        }
                    );

                    context.SaveChanges(); // Persist changes
                }
            }
        }
    }
}

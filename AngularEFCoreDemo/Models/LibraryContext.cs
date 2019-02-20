using Microsoft.EntityFrameworkCore;

namespace AngularEFCoreDemo.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookEdition> BookEditions { get; set; }
        public DbSet<AuthorityEntry> AuthorityEntries { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ReaderTicket> ReaderTickets { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<BookTakeEvent> BookTakeEvents { get; set; }
    }
}

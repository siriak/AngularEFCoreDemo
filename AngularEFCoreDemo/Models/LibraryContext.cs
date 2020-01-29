using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public List<Person> GetLibrarians() => People.Where(p => p.Role == Role.Librarian).ToList();
        
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }

            var deleted = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && ((dynamic) x.Entity).IsDeleted)
                .Select(x => (dynamic)x.Entity).ToList();
            foreach (var entity in deleted)
            {
                MarkDeleted(entity);
            }

            return base.SaveChanges();
        }
        
        private void MarkDeleted(ReaderTicket readerTicket)
        {
            readerTicket.IsDeleted = true;
            foreach (var bookTakeEvent in BookTakeEvents.Where(bte => bte.TicketId == readerTicket.ReaderTicketId && !bte.IsDeleted))
            {
                MarkDeleted(bookTakeEvent);
            }
        }

        private void MarkDeleted(BookEdition bookEdition)
        {
            bookEdition.IsDeleted = true;
            foreach (var book in Books.Where(b => b.BookEditionId == bookEdition.BookEditionId && !b.IsDeleted))
            {
                MarkDeleted(book);
            }
        }

        private void MarkDeleted(Book book)
        {
            book.IsDeleted = true;
            foreach (var bookTakeEvent in BookTakeEvents.Where(bte => bte.BookId == book.BookId && !bte.IsDeleted))
            {
                MarkDeleted(bookTakeEvent);
            }
        }

        private void MarkDeleted(Section section)
        {
            section.IsDeleted = true;
            foreach (var book in Books.Where(b => b.SectionId == section.SectionId && !b.IsDeleted))
            {
                MarkDeleted(book);
            }
        }

        private void MarkDeleted(AuthorityEntry authorityEntry) => authorityEntry.IsDeleted = true;

        private void MarkDeleted(BookTakeEvent bookTakeEvent) => bookTakeEvent.IsDeleted = true;
    }
}

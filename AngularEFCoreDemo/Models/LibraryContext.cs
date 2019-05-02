using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
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

        public void MarkDeleted(Person person)
        {
            person.IsDeleted = true;

            foreach (var authorityEntry in AuthorityEntries.Where(ae => ae.PersonId == person.PersonId))
            {
                MarkDeleted(authorityEntry);
            }

            foreach (var readerTicket in ReaderTickets.Where(rt => rt.ReaderId == person.PersonId))
            {
                MarkDeleted(readerTicket);
            }
        }

        public void MarkDeleted(ReaderTicket readerTicket)
        {
            readerTicket.IsDeleted = true;

            foreach (var bookTakeEvent in BookTakeEvents.Where(bte => bte.TicketId == readerTicket.ReaderTicketId))
            {
                MarkDeleted(bookTakeEvent);
            }
        }

        public void MarkDeleted(BookEdition bookEdition)
        {
            bookEdition.IsDeleted = true;

            foreach (var book in Books.Where(b => b.BookEditionId == bookEdition.BookEditionId))
            {
                MarkDeleted(book);
            }
        }

        public void MarkDeleted(Book book)
        {
            book.IsDeleted = true;

            foreach (var bookTakeEvent in BookTakeEvents.Where(bte => bte.BookId == book.BookId))
            {
                MarkDeleted(bookTakeEvent);
            }
        }

        public void MarkDeleted(Section section)
        {
            section.IsDeleted = true;

            foreach (var book in Books.Where(b => b.SectionId == section.SectionId))
            {
                MarkDeleted(book);
            }
        }

        public void MarkDeleted(AuthorityEntry authorityEntry) => authorityEntry.IsDeleted = true;

        public void MarkDeleted(BookTakeEvent bookTakeEvent) => bookTakeEvent.IsDeleted = true;
    }
}

using AngularEFCoreDemo.Models;
using System;
using System.Linq;

namespace AngularEFCoreDemo
{
    internal static class DBInitializer
    {
        public static void Seed(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Books.Any())
            {
                var king = new Person
                {
                    FirstName = "Stephen",
                    LastName = "King",
                    BirthDate = new DateTime(1947, 9, 21),
                    Role = Role.None,
                };

                var bookIt = new BookEdition
                {
                    Title = "It",
                };

                var authEntry = new AuthorityEntry
                {
                    Order = 1,
                    Person = king,
                    BookEditionId = 1,
                };

                var section = new Section
                {
                    Building = 1,
                    Room = 2,
                    Cabinet = 3,
                    Shelf = 4,
                };

                var pit1 = new Book
                {
                    BookEdition = bookIt,
                    Section = section,
                };

                var pit2 = new Book
                {
                    BookEdition = bookIt,
                    Section = section,
                };

                var pit3 = new Book
                {
                    BookEdition = bookIt,
                    Section = section,
                };

                var me = new Person
                {
                    FirstName = "Andrii",
                    LastName = "Siriak",
                    BirthDate = new DateTime(1999, 9, 2),
                    Role = Role.Student,
                };

                var myTicket = new ReaderTicket
                {
                    CreationDate = new DateTime(2017, 9, 1),
                    ExpirationDate = new DateTime(2017, 9, 1).AddYears(5),
                    Reader = me,
                };

                var marivanna = new Person
                {
                    FirstName = "Marija",
                    LastName = "Ivanova",
                    BirthDate = new DateTime(1914, 9, 2),
                    Role = Role.Librarian,
                };

                var bookTake = new BookTakeEvent
                {
                    Book = pit1,
                    ExpectedReturnDate = DateTime.Today.AddMonths(1),
                    GiveDate = DateTime.Today,
                    Person = marivanna,
                    Ticket = myTicket,
                };

                context.BookEditions.Add(bookIt);
                context.Books.AddRange(pit1, pit2, pit3);
                context.People.AddRange(king, me, marivanna);
                context.ReaderTickets.Add(myTicket);
                context.BookTakeEvents.Add(bookTake);
                context.Sections.Add(section);
                context.AuthorityEntries.Add(authEntry);
            }

            context.SaveChanges();
        }
    }
}

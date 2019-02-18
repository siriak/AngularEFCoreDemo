using System;

namespace AngularEFCoreDemo.Models
{
    public class BookTakeEvent
    {
        public int BookTakeEventId { get; set; }
        public int BookId { get; set; }
        public int TicketId { get; set; }
        public int PersonId { get; set; }
        public DateTime GiveDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public bool IsDeleted { get; set; }

        public Book Book { get; set; }
        public Person Person { get; set; }
        public ReaderTicket Ticket { get; set; }
    }
}

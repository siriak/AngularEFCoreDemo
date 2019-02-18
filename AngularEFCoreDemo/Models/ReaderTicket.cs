using System;

namespace AngularEFCoreDemo.Models
{
    public class ReaderTicket
    {
        public int ReaderTicketId { get; set; }
        public int ReaderId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsDeleted { get; set; }

        public Person Reader { get; set; }
    }
}

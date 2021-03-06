﻿namespace AngularEFCoreDemo.Models
{
    public class AuthorityEntry
    {
        public int AuthorityEntryId { get; set; }
        public int Order { get; set; }
        public int PersonId { get; set; }
        public int BookEditionId { get; set; }
        public bool IsDeleted { get; set; }

        public Person Person { get; set; }
        public BookEdition BookEdition { get; set; }
    }
}
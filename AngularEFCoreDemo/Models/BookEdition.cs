using System.Collections.Generic;

namespace AngularEFCoreDemo.Models
{
    public class BookEdition
    {
        public int BookEditionId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<AuthorityEntry> Authors { get; set; }
    }
}

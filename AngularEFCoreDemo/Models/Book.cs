namespace AngularEFCoreDemo.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int BookEditionId { get; set; }
        public int SectionId { get; set; }
        public bool IsDeleted { get; set; }

        public BookEdition BookEdition { get; set; }
        public Section Section { get; set; }
    }
}

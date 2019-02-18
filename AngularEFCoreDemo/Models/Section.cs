namespace AngularEFCoreDemo.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public int Building { get; set; }
        public int Room { get; set; }
        public int Cabinet { get; set; }
        public int Shelf { get; set; }
        public bool IsDeleted { get; set; }
    }
}

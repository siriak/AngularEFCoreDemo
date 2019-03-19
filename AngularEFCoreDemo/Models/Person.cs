using System;

namespace AngularEFCoreDemo.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}

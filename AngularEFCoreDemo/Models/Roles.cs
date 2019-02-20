using System;

namespace AngularEFCoreDemo.Models
{
    [Flags]
    public enum Roles
    {
        None      = 0b0000,
        Student   = 0b1000,
        Teacher   = 0b0100,
        Librarian = 0b0010,
        Author    = 0b0001,
    }
}

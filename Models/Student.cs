using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nkuli.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayName("School name")]
        public string SchoolName { get; set; }

        public string FullName()
        {
            return Name + " " + Surname;
        }
    }
}

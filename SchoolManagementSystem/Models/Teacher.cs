using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public  class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }= new List<Course>();
    }
}

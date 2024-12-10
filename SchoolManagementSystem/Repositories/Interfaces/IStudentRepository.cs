using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories.Interfaces
{
   public interface IStudentRepository :IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsWithCoursesAsync();
        Task<Student> GetStudentByNameAsync(string name);
    }
}

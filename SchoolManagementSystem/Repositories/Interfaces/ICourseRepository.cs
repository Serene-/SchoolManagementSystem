using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories.Interfaces
{
     public interface ICourseRepository :IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesWithStudentsAsync();
        Task<IEnumerable<Course>> GetCoursesWithTeachersAsync();
        Task<Course> GetCourseByNameAsync(string name);
    }
}

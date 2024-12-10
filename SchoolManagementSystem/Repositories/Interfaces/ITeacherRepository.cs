using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories.Interfaces
{
    public interface ITeacherRepository :IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersWithCoursesAsync();
        Task<Teacher> GetTeacherByNameAsync(string name);
    }
}

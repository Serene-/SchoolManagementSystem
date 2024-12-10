using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<Student> GetStudentByNameAsync(string name)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Student>> GetStudentsWithCoursesAsync()
        {
            return await _context.Students
                             .Include(s => s.Enrollments)
                             .ThenInclude(e => e.Course)
                             .ToListAsync();
        }
    }
}

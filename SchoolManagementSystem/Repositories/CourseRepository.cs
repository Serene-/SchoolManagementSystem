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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetCoursesWithStudentsAsync()
        {
           return await _context.Courses
                .Include(x => x.Enrollments)
                .ThenInclude(y => y.StudentId)
                .ToListAsync();
              
        }

        public async Task<IEnumerable<Course>> GetCoursesWithTeachersAsync()
        {
            return await _context.Courses
                 .Include(x => x.Teacher)
                 .ToListAsync();
        }
        public async Task<Course> GetCourseByNameAsync(string name)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(x => x.Name.ToLower()==name.ToLower());
        } 

    }
}

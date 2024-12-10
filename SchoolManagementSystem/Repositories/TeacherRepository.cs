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
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teacher>> GetTeachersWithCoursesAsync()
        {
            return await _context.Teachers
                .Include(x=>x.Courses)
                .ToListAsync();
                
        }

        public async Task<Teacher> GetTeacherByNameAsync(string name)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}

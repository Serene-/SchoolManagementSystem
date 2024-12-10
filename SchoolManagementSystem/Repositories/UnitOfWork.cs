using SchoolManagementSystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;
        public IStudentRepository StudentRepository { get; private set; }

        public ICourseRepository CourseRepository { get; private set; }

        public ITeacherRepository TeacherRepository { get; private set; }
        public IEnrollmentRepository EnrollmentRepository { get; private set; }
        public UnitOfWork(SchoolContext context)
        {
            _context = context;
            StudentRepository = new StudentRepository(_context);
            CourseRepository = new CourseRepository(_context);  
            TeacherRepository = new TeacherRepository(_context);  
            EnrollmentRepository = new EnrollmentRepository(_context);
        }

        public void Dispose()
        {
           _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

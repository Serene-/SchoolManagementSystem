using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        Task<int> SaveChangesAsync();
    }
}


using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using System.Threading.Channels;

namespace SchoolManagementSystem
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            SchoolContext context = new SchoolContext();
            Console.WriteLine("Welcome to the School Management System");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Course");
            Console.WriteLine("3. Add Teacher");
            Console.WriteLine("4. Enroll a Student");
            Console.WriteLine("5. Assign Grades");
            Console.WriteLine("6. View Student Report");
            Console.WriteLine("7. View All Courses");
            Console.WriteLine("8. Exit");
            string input=Console.ReadLine();
            while(input!="8")
            {
                switch(input)
                {
                    case "1":
                        {
                            AddStudent(context);
                            break;
                        }
                    case "2":
                        {
                            await AddCourseAsync(context);
                            break;
                        }
                    case "3":
                        {
                            AddTeacher(context);
                            break;
                        }
                    case "4":
                        {
                            await EnrollStudent(context);
                            break;
                        }
                    case "5":
                        {
                           await AssignGrades(context);
                            break;
                        }
                    case "6":
                        {
                            await StudentReport(context);
                            break;
                        }
                    case "7":
                        {
                            await AllCourses(context);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input!!");
                            break;
                        }

                }
                input= Console.ReadLine();  
            }


        }

        private static async Task AssignGrades(SchoolContext context)
        {
           var unitOfWork=new UnitOfWork(context);
            Console.WriteLine("Enter the student name");
            string studenName=Console.ReadLine();
            Student student=await unitOfWork.StudentRepository.GetStudentByNameAsync(studenName); 
            Console.WriteLine("Enter the course");
            string courseName=Console.ReadLine();
            Course course=await unitOfWork.CourseRepository.GetCourseByNameAsync(courseName);
            bool flag = true;
            Grade grade =Grade.A;
            while (flag)
            { 
            Console.WriteLine("Enter the grade (A/B/C/D/F)");
            string gradeInput=Console.ReadLine();
            
                switch (gradeInput)
                {
                    case "A":
                        {
                            grade = Grade.A;
                            flag = false;
                            break;
                        }
                    case "B":
                        {
                            grade = Grade.B;
                            flag = false;
                            break;
                        }
                    case "C":
                        {
                            grade = Grade.C;
                            flag = false;
                            break;
                        }
                    case "D":
                        {
                            grade = Grade.D;
                            flag = false;
                            break;
                        }
                    case "F":
                        {
                            grade = Grade.F;
                            flag = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input! Please enter correct value:");
                            flag = true;
                            break;
                        }
                }
            }
            if (student != null && course != null)
            {
                var enrollList = await unitOfWork.EnrollmentRepository
                    .GetAllAsync();
                var enroll=enrollList.FirstOrDefault(y => (y.CourseId == course.CourseID) && (y.StudentId == student.StudentId));
                if (enroll != null)
                {
                    enroll.Grade = grade;
                    unitOfWork.EnrollmentRepository.Update(enroll);
                    await unitOfWork.EnrollmentRepository.SaveChangesAsync();
                    Console.WriteLine("Grade is succussfully assigned!");
                }
            }

        }

        private static async Task EnrollStudent(SchoolContext context)
        {
            var unitOfWork=new UnitOfWork(context);
            Console.WriteLine("Enter the student name:");
            string studentName = Console.ReadLine();
            var student=await unitOfWork.StudentRepository.GetStudentByNameAsync(studentName);
            Console.WriteLine("Enter the course:");
            string courseName = Console.ReadLine();
            var course=await unitOfWork.CourseRepository.GetCourseByNameAsync(courseName);
            if (student != null && course != null)
            {
                Enrollment enroll = new Enrollment()
                {
                    StudentId = student.StudentId,
                    CourseId= course.CourseID,
                };
                await unitOfWork.EnrollmentRepository.AddAsync(enroll);
                await unitOfWork.EnrollmentRepository.SaveChangesAsync();
                Console.WriteLine("Student is successfully enrolled!");
            }
            else
            {
                Console.WriteLine("Student is not enrolled!");
            }
        }

        private static async Task AllCourses(SchoolContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            var courses = await unitOfWork.CourseRepository.GetCoursesWithTeachersAsync();
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - Mr/Mrs {course.Teacher.Name}");
            }
        }

        private static async Task StudentReport(SchoolContext context)
        {
           var unitOfwork=new UnitOfWork(context);
           var students=await unitOfwork.StudentRepository.GetStudentsWithCoursesAsync();
           foreach(var student in students)
            {
                Console.WriteLine($"{student.Name}");
                foreach(var enroll in student.Enrollments)
                {
                    Console.WriteLine($"  {enroll.Course.Name} - {enroll.Grade}");
                }
            }

        }

        private static void AddTeacher(SchoolContext context)
        {
            Console.WriteLine("Enter the name of the teacher");
            string name=Console.ReadLine();
            Teacher teacher = new Teacher()
            {
                Name = name
            };
            var unitOfWork = new UnitOfWork(context);
            unitOfWork.TeacherRepository.AddAsync(teacher);
            unitOfWork.TeacherRepository.SaveChangesAsync();
         

        }

        private static async Task AddCourseAsync(SchoolContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            Console.WriteLine("Enter the course name:");
            string courseName = Console.ReadLine();
            Course course = new Course()
            {
                Name = courseName
            };
            Console.WriteLine("Enter the teacher name of the course:");
            string teacherName = Console.ReadLine();
            Teacher teacher = await unitOfWork.TeacherRepository.GetTeacherByNameAsync(teacherName);
            if(teacher!=null)
            {
                course.Teacher = teacher;
            }
            else
            {
                Teacher teacherNew = new Teacher()
                {
                    Name = teacherName
                };
                await unitOfWork.TeacherRepository.AddAsync(teacherNew);
                await unitOfWork.TeacherRepository.SaveChangesAsync();
                course.Teacher =  await unitOfWork.TeacherRepository.GetTeacherByNameAsync(teacherNew.Name);
            }
            await unitOfWork.CourseRepository.AddAsync(course);
            await unitOfWork.CourseRepository.SaveChangesAsync();
        }

        private static void AddStudent(SchoolContext context)
        {
            SchoolContext _context = context;
            Console.WriteLine("Enter the student name:");
            string name=Console.ReadLine();
            Student student = new Student()
            {
                Name=name,
                EnrollmentDate=DateTime.Now
            };
            var unitOfWork = new UnitOfWork(_context);
            unitOfWork.StudentRepository.AddAsync(student);
            unitOfWork.StudentRepository.SaveChangesAsync();
        }
    }
}

using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string searchString);
        Task<ResponseEnum> CreateCourseAsync(Course course);
        Task<ResponseEnum> UpdateCourseAsync(int id, Course course);
        Task<ResponseEnum> DeleteCourseAsync(int id);
        Task<ResponseEnum> AddStudentToCourseAsync(StudentCourse studentWithCourse);
        Task<ResponseEnum> DeleteStudentFromCourseAsync(StudentCourse studentWithCourse);
        Task<ResponseEnum> AddTeacherToCourseAsync(TeacherCourse teacherWithCourse);
        Task<ResponseEnum> DeleteTeacherFromCourseAsync(TeacherCourse teacherWithCourse);
    }
}

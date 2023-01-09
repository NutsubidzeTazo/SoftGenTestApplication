using SoftGen.Domain.Entities;
using SoftGen.Repository.Interfaces;
using SoftGen.Repository.Repositories;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SoftGen.Services.BusinessServices
{
    public class CourseService : ICourseService
    {
        private readonly IBaseRepository<Course> _courseBaseRepository;
        private readonly IBaseRepository<Student> _studentBaseRepository;
        private readonly IBaseRepository<Teacher> _teacherBaseRepository;
        private readonly IBaseRepository<StudentCourse> _studenCourseRepository;
        private readonly IBaseRepository<TeacherCourse> _teacherCourseRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseService(IBaseRepository<Course> courseRepository,
            IBaseRepository<Student> studentBaseRepository,
            IBaseRepository<Teacher> teacherBaseRepository, 
            ICourseRepository course, IBaseRepository<StudentCourse> 
            studenCourseRepository, IBaseRepository<TeacherCourse> 
            teacherCourseRepository)
        {
            _courseBaseRepository = courseRepository;
            _studentBaseRepository = studentBaseRepository;
            _teacherBaseRepository = teacherBaseRepository;
            _courseRepository = course;
            _studenCourseRepository = studenCourseRepository;
            _teacherCourseRepository = teacherCourseRepository;
        }

        public async Task<ResponseEnum> CreateCourseAsync(Course course)
        {
            try
            {
                await _courseBaseRepository.InsertAsync(course);

                return ResponseEnum.Success;
            }
            catch (Exception)
            {

                return ResponseEnum.Failed;
            }
        }

        public async Task<ResponseEnum> DeleteCourseAsync(int id)
        {
            if (id > 0)
            {
                var student = await _courseBaseRepository.GetAsync(id);
                if (student != null)
                {
                    student.IsDeleted = true;
                    await _courseBaseRepository.UpdateAsync(student);

                    return ResponseEnum.Success;
                }
            }
            return ResponseEnum.NotFound;

        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(string? searchString)
        {
            try
            {
                var courses = await _courseBaseRepository.SearchAsync(searchString);
                return courses;
            }
            catch (Exception)
            {

                throw new Exception("Course not found");
            }
        }

        public async Task<ResponseEnum> UpdateCourseAsync(int Id, Course course)
        {
            if (Id > 0 && course != null)
            {
                var studentToUpdate = await _courseBaseRepository.GetAsync(Id);

                studentToUpdate.CourseName = course.CourseName;
                studentToUpdate.CourseGrade = course.CourseGrade;

                await _courseBaseRepository.UpdateAsync(studentToUpdate);
                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }
        }

        public async Task<ResponseEnum> AddStudentToCourseAsync(StudentCourse studentWithCourse)
        {
            if (studentWithCourse != null)
            {
                await _courseRepository.AddStudentToCourseAsync(studentWithCourse);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }
        }

        public async Task<ResponseEnum> DeleteStudentFromCourseAsync(StudentCourse studentWithCourse)
        {
            var student = await _studentBaseRepository.GetAsync(studentWithCourse.StudentId);
            var course = await _courseBaseRepository.GetAsync(studentWithCourse.CourseId);
            var checkStudentCourse = await _studenCourseRepository.GetAsync(studentWithCourse.Id);

            if (student != null && course != null && checkStudentCourse != null)
            {
                await _studenCourseRepository.DeleteAsync(studentWithCourse);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.NotFound;
            }
        }

        public async Task<ResponseEnum> AddTeacherToCourseAsync(TeacherCourse teacherWithCourse)
        {
            if (teacherWithCourse != null)
            {
                await _courseRepository.AddTeacherToCourseAsync(teacherWithCourse);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }

        }
        public async Task<ResponseEnum> DeleteTeacherFromCourseAsync(TeacherCourse teacherWithCourse)
        {
            var teacher = await _teacherBaseRepository.GetAsync(teacherWithCourse.TeacherId);
            var course = await _courseBaseRepository.GetAsync(teacherWithCourse.CourseId);
            var checkTeacherCourse = await _teacherCourseRepository.GetAsync(teacherWithCourse.Id);

            if (teacher != null && course != null && checkTeacherCourse != null)
            {
                await _teacherCourseRepository.DeleteAsync(teacherWithCourse);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.NotFound;
            }
        }

    }
}

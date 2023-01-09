using Microsoft.EntityFrameworkCore;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly SoftGenApplicationDbContext _context;
        public CourseRepository(SoftGenApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public override async Task<IEnumerable<Course>> SearchAsync(string? searchString)
        {
            var query = _context.Courses.AsNoTracking().Where(c => c.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.CourseName.Contains(searchString) || c.CourseGrade.Contains(searchString));
            }
            return await query.ToListAsync();
        }

        public async Task AddStudentToCourseAsync(StudentCourse studentCourse)
        {
            await _context.StudentCourses.AddAsync(studentCourse);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStudentToCourseAsync(StudentCourse studentCourse)
        {
            await _context.StudentCourses.AddAsync(studentCourse);
            await _context.SaveChangesAsync();
        }
        public async Task AddTeacherToCourseAsync(TeacherCourse teacherCourse)
        {
            await _context.TeacherCourses.AddAsync(teacherCourse);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeacherToCourseAsync(TeacherCourse teacherCourse)
        {
            await _context.TeacherCourses.AddAsync(teacherCourse);
            await _context.SaveChangesAsync();
        }
    }
}

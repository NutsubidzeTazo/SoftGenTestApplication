using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? CourseGrade { get; set; }
        public bool? IsDeleted { get; set; } = false;

        public  ICollection<StudentCourse> Students { get; set; }
        public  ICollection<TeacherCourse> Teachers { get; set; }
    }
}

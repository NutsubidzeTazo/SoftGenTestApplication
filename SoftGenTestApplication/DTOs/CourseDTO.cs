using System.ComponentModel;

namespace SoftGenTestApplication.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? CourseGrade { get; set; }
        [DefaultValue(false)]
        public bool? IsDeleted { get; set; } 
    }
}

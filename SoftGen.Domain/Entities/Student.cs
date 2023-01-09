using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PersonalNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
    }
}

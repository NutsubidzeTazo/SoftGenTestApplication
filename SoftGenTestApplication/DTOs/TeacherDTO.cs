using System.ComponentModel;

namespace SoftGenTestApplication.DTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}

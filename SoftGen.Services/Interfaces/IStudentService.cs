using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string searchString);
        Task<ResponseEnum> CreateStudentAsync(Student student);
        Task<ResponseEnum> UpdateStudentAsync(int id, Student student);
        Task<ResponseEnum> DeleteStudentAsync(int id);

    }
}

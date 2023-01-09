using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.Interfaces
{
    public interface ITeacherService
    {

        Task<IEnumerable<Teacher>> GetTeachersAsync(string searchString);
        Task<ResponseEnum> CreateTeacherAsync(Teacher teacher);
        Task<ResponseEnum> UpdateTeacherAsync(int id, Teacher teacher);
        Task<ResponseEnum> DeleteTeacherAsync(int id);
    }
}

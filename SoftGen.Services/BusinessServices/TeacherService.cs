using SoftGen.Domain.Entities;
using SoftGen.Repository.Interfaces;
using SoftGen.Repository.Repositories;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.BusinessServices
{
    public class TeacherService : ITeacherService
    {
        private readonly IBaseRepository<Teacher> _teacherRepository;
        public TeacherService(IBaseRepository<Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<ResponseEnum> CreateTeacherAsync(Teacher teacher)
        {
            try
            {
                await _teacherRepository.InsertAsync(teacher);
                return ResponseEnum.Success;
            }
            catch (Exception)
            {
                return ResponseEnum.Failed;
            }
        }

        public async Task<ResponseEnum> DeleteTeacherAsync(int id)
        {
            if (id > 0)
            {
                var student = await _teacherRepository.GetAsync(id);
                if (student != null)
                {
                    student.IsDeleted = true;
                    await _teacherRepository.UpdateAsync(student);

                    return ResponseEnum.Success;
                }
            }
            return ResponseEnum.NotFound;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync(string? searchString)
        {
            try
            {
                var teachers = await _teacherRepository.SearchAsync(searchString);
                return teachers;
            }
            catch (Exception)
            {

                throw new Exception("Teacher not found");
            }
        }

        public async Task<ResponseEnum> UpdateTeacherAsync(int Id, Teacher teacher)
        {
            if (Id > 0 && teacher != null)
            {
                var teacherToUpdate = await _teacherRepository.GetAsync(Id);

                teacherToUpdate.DateOfBirth = teacher.DateOfBirth;
                teacherToUpdate.FirstName = teacher.FirstName;
                teacherToUpdate.LastName = teacher.LastName;
                teacherToUpdate.PersonalNumber = teacher.PersonalNumber;
                teacherToUpdate.Email = teacher.Email;

                await _teacherRepository.UpdateAsync(teacherToUpdate);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }
        }

    }
}

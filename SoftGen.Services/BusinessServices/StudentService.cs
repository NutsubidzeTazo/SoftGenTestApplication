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
    public class StudentService : IStudentService
    {
        private readonly IBaseRepository<Student> _studentRepository;
        public StudentService(IBaseRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResponseEnum> CreateStudentAsync(Student student)
        {
            try
            {
                await _studentRepository.InsertAsync(student);
                return ResponseEnum.Success;
            }
            catch (Exception)
            {
                return ResponseEnum.Failed;
            }
        }

        public async Task<ResponseEnum> DeleteStudentAsync(int id)
        {
            if (id > 0)
            {
                var student = await _studentRepository.GetAsync(id);
                if (student != null)
                {
                    student.IsDeleted = true;
                    await _studentRepository.UpdateAsync(student);

                    return ResponseEnum.Success;
                }
            }
            return ResponseEnum.NotFound;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string? searchString)
        {
            try
            {
                var students = await _studentRepository.SearchAsync(searchString);
                return students;
            }
            catch (Exception)
            {

                throw new Exception("Student not found");
            }

        }

        public async Task<ResponseEnum> UpdateStudentAsync(int Id, Student student)
        {
            if (Id > 0 && student != null)
            {
                var studentToUpdate = await _studentRepository.GetAsync(Id);

                studentToUpdate.DateOfBirth = student.DateOfBirth;
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.PersonalNumber = student.PersonalNumber;
                studentToUpdate.Email = student.Email;

                await _studentRepository.UpdateAsync(studentToUpdate);

                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }
        }
    }
}

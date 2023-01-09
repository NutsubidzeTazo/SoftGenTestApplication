using AutoMapper;
using SoftGen.Domain.Entities;
using SoftGenTestApplication.DTOs;

namespace SoftGenTestApplication.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<CourseDTO, Course>();
            CreateMap<StudentCourseDTO, StudentCourse>();
            CreateMap<TeacherCourseDTO, TeacherCourse>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Student,StudentDTO>();
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}

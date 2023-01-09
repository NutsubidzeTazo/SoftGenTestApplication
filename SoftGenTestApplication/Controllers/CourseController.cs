using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using SoftGenTestApplication.DTOs;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftGenTestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> SearchCoursesAsync([FromQuery] string? searchString)
        {
            var courses = await _courseService.GetCoursesAsync(searchString);
            var coursesDTO = _mapper.Map<IEnumerable<CourseDTO>>(courses);

            return Ok(coursesDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourseAsync([FromBody] CourseDTO courseDTO)
        {
            var result = await _courseService.CreateCourseAsync(_mapper.Map<Course>(courseDTO));
            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourseAsync(int courseId, [FromBody] CourseDTO courseDTO)
        {
            var result = await _courseService.UpdateCourseAsync(courseId, _mapper.Map<Course>(courseDTO));
            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCourseAsync(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPut("student")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] StudentCourseDTO studentCourseDTO)
        {
            var result = await _courseService.AddStudentToCourseAsync(_mapper.Map<StudentCourse>(studentCourseDTO));
            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("student")]
        public async Task<IActionResult> DeleteStudentFromCourseAsync([FromBody] StudentCourseDTO studentCourseDTO)
        {
            var result = await _courseService.DeleteStudentFromCourseAsync(_mapper.Map<StudentCourse>(studentCourseDTO));

            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPut("teacher")]
        public async Task<IActionResult> AddTeacherToCourseAsync([FromBody] TeacherCourseDTO teacherCourseDTO)
        {
             var result = await _courseService.AddTeacherToCourseAsync(_mapper.Map<TeacherCourse>(teacherCourseDTO));

            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("teacher")]
        public async Task<IActionResult> DeleteTeacherFromCourseAsync([FromBody] TeacherCourseDTO teacherCourseDTO)
        {
            var result = await _courseService.DeleteTeacherFromCourseAsync(_mapper.Map<TeacherCourse>(teacherCourseDTO));

            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}

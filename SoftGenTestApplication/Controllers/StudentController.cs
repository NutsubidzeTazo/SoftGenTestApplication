using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using SoftGenTestApplication.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftGenTestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync([FromQuery] string? searchString)
        {
            var students = await _studentService.GetStudentsAsync(searchString);
            var studentsDTO = _mapper.Map<StudentDTO>(students);

            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentDTO studentDTO)
        {
            var result = await _studentService.CreateStudentAsync(_mapper.Map<Student>(studentDTO));
            if (result == ResponseEnum.Success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] StudentDTO studentDTO)
        {
            var result = await _studentService.UpdateStudentAsync(id, _mapper.Map<Student>(studentDTO));
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
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
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

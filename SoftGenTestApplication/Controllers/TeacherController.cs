using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using SoftGenTestApplication.DTOs;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftGenTestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDTO))]
        public async Task<IActionResult> SearchTeachersAsync([FromQuery] string? searchText)
        {
            var teachers = await _teacherService.GetTeachersAsync(searchText);
            var teachersDTO = _mapper.Map<TeacherDTO>(teachers);
            
            return Ok(teachers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddTeacherAsync([FromBody] TeacherDTO teacherDTO)
        {
            var result = await _teacherService.CreateTeacherAsync(_mapper.Map<Teacher>(teacherDTO));
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTeacherAsync(int id, [FromBody] TeacherDTO teacherDTO)
        {
            var result = await _teacherService.UpdateTeacherAsync(id, _mapper.Map<Teacher>(teacherDTO));
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeacherAsync(int id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
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

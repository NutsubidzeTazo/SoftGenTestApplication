using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Interfaces;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using SoftGenTestApplication.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftGenTestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private string generatedToken = null;
        public UserController(IUserService userService, ITokenService tokenService, IConfiguration config, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _config = config;
            _mapper = mapper;
        }

        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.UserName) || string.IsNullOrEmpty(userDTO.Password))
            {
                return  Ok();
            }
            IActionResult response = Unauthorized();

            var validUser = await _userService.GetUserAsync(_mapper.Map<User>(userDTO));

            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("{personId}/user")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO userDTO)
        {
            var result = await  _userService.RegisterUserAsync(_mapper.Map<User>(userDTO));
            if (result == ResponseEnum.Success)
            {

                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{

        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

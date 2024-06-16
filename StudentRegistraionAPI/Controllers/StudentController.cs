using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRegistraionAPI.Data.Interfaces;
using StudentRegistraionAPI.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRegistraionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student studentRequest)
        {
            var student = await _studentRepository.AddStudent(studentRequest);

            return Ok(student);
        }

        [HttpPut("EditStudent")]
        public async Task<IActionResult> EditStudent([FromBody] Student studentRequest)
        {
            var student = await _studentRepository.EditStudent(studentRequest);
            return Ok(student);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentRepository.DeleteStudent(id);
            return Ok(result);
        }

        [HttpGet("GetStudentDetailsById")]
        public async Task<IActionResult> GetStudentDetailsById(int studentId)
        {
            var student = await _studentRepository.GetStudentDetailsById(studentId);
            return Ok(student);
        }

        [HttpGet("GetProfileImageUrl/{studentId}")]
        public async Task<IActionResult> GetProfileImageUrl(int studentId)
        {
            try
            {
                var imageUrl = await _studentRepository.GetProfileImageUrl(studentId);
                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

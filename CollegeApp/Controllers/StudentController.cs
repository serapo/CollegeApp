using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All",Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            //var students = new List<StudentDTO>();
            //foreach( var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email,

            //    };
            //    students.Add(obj);
            //}
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
                {
                    Id = s.Id,
                    StudentName = s.StudentName,
                    Address = s.Address,
                    Email = s.Email,
                    
                });

            //OK - 200 - Success
            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentsById(int id)
        {
            //BadRequest - 400 Badrequest - Client error
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"the strudent with id {id} not found");
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
            };
            //Ok - 200 - Success
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudenByName(string name)
        {
            //BadRequest - 400 Badrequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"the strudent with id {name} not found");
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
            };
            //Ok - 200 - Success
            return Ok(studentDTO);
          
        }
        
        
        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            int newId = CollegeRepository.Students.LastOrDefault()?.Id + 1 ?? 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
                
            };
            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            // Status  - 201
            //https://localhost:7115/api/Student/1
            //New student details
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);
           
        }
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //BadRequest - 400 Badrequest - Client error
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"the strudent with id {id} not found");

            CollegeRepository.Students.Remove(student);
            //Ok - 200 - Success
            return Ok(true);
        }
    }
}

using API.DTOs.Student;
using API.Models;
using API.Services.Contract;
using API.Services.Implementation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        public StudentController(
            ILogger<StudentController> logger,
            IMapper mapper,
            IStudentService studentService
        )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                List<StudentDTO> dTOs = this._mapper.Map<List<StudentDTO>>(await _studentService.List());
                if (dTOs.Count > 0)
                {
                    return Ok(
                        new {
                            code = "SUCCESSFUL_OPERATION",
                            message = "Successful Operation",
                            students = dTOs
                        });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }

            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de estudiantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                StudentDTO studentDTO = this._mapper.Map<StudentDTO>(await this._studentService.Get(id));
                if (studentDTO == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }
                return Ok(new
                {
                    code = "SUCCESSFUL_OPERATION",
                    message = "Successful Operation",
                    student = studentDTO
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> save([FromBody] StudentSaveDTO studentDTO)
        {
            try
            {

                Student model = this._mapper.Map<Student>(studentDTO);
                StudentDTO dto = this._mapper.Map<StudentDTO>(await this._studentService.Save(model));
                if (dto.Id == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "BAD_REQUEST",
                        message = "Bad request"
                    });
                }
                return Ok(new
                {
                    code = "CREATED",
                    message = "Created resource",
                    student = dto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] StudentUpdateDTO studentDTO)
        {
            try
            {
                Student studentBD = await this._studentService.Get(id);
                if(studentBD == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }
                Student model = this._mapper.Map<Student>(studentDTO);
                studentBD.Name = model.Name;
                studentBD.Age = model.Age;
                studentBD.Address = model.Address;

                bool response = await this._studentService.Update(studentBD);
                if (!response)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        code = "INTERNAL_SERVER_ERROR",
                        message = "Internal server error"
                    });
                }
                return Ok(new
                {
                    code = "SUCCESSFUL_OPERATION",
                    message = "Successful Operation",
                    student = this._mapper.Map<StudentDTO>(studentBD)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar un estudiantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                Student studentBD = await this._studentService.Get(id);
                if (studentBD == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }
                bool response = await this._studentService.Delete(studentBD);
                if (!response)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        code = "INTERNAL_SERVER_ERROR",
                        message = "Internal server error"
                    });
                }

                return Ok(new
                {
                    code = "SUCCESSFUL_OPERATION",
                    message = "Successful Operation"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar un estudiante.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }

        [HttpGet("{studentId}/qualifications")]
        public async Task<IActionResult> Details(int studentId)
        {
            try
            {
                StudentsQualificationsDTO studentsDTO = this._mapper.Map<StudentsQualificationsDTO>(await this._studentService.Details(studentId));
                if (studentsDTO == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }


                return Ok(new
                {
                    code = "SUCCESSFUL_OPERATION",
                    message = "Successful Operation",
                    student = studentsDTO
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }
    }
}

using API.DTOs.Student;
using API.Models;
using API.Services.Contract;
using API.Utility;
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
        private readonly string identifier;
        public StudentController(
            ILogger<StudentController> logger,
            IMapper mapper,
            IStudentService studentService
        )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._studentService = studentService;
            this.identifier = "xxxxxxxxxx";
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                List<StudentDTO> dTOs = this._mapper.Map<List<StudentDTO>>(await _studentService.List());
                if (dTOs.Count <= 0)
                {
                    return ResponseHandler.NotFoundResponse();

                }

                return ResponseHandler.success(this.identifier, dTOs);

            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de estudiantes.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }
                return ResponseHandler.success(identifier, studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.badRequestResponse();
                }
                return ResponseHandler.created(identifier, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }
                Student model = this._mapper.Map<Student>(studentDTO);
                studentBD.Name = model.Name;
                studentBD.Age = model.Age;
                studentBD.Address = model.Address;

                bool response = await this._studentService.Update(studentBD);
                if (!response)
                {
                    return ResponseHandler.internalServerError();
                }
                return ResponseHandler.success(identifier, this._mapper.Map<StudentDTO>(studentBD));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar un estudiantes.");
                return ResponseHandler.internalServerError();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                Student studentBD = await this._studentService.Details(id);
                if (studentBD == null)
                {
                    return ResponseHandler.NotFoundResponse();
                }

                bool response = await this._studentService.Delete(studentBD);
                if (!response)
                {
                    return ResponseHandler.internalServerError();
                }

                return ResponseHandler.success(identifier, studentBD);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar un estudiante.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }


                return ResponseHandler.success(identifier,studentsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un estudiantes.");
                return ResponseHandler.internalServerError();
            }
        }
    }
}

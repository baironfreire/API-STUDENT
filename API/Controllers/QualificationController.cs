using API.DTOs.Qualification;
using API.Models;
using API.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/qualifications")]
    public class QualificationController : Controller
    {
        private readonly ILogger<QualificationController> _logger;
        private readonly IMapper _mapper;
        private readonly IQualificationService _qualificationService;

        public QualificationController(
            ILogger<QualificationController> logger,
            IMapper mapper,
            IQualificationService qualificationService
        )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._qualificationService = qualificationService;
            
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                List<QualificationDTO> dTOs = this._mapper.Map<List<QualificationDTO>>(await _qualificationService.List());
                if (dTOs.Count > 0)
                {
                    return Ok(
                        new
                        {
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

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de cualificacion.");
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
                QualificationDTO dto = this._mapper.Map<QualificationDTO>(await this._qualificationService.Get(id));
                if (dto == null)
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
                    student = dto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener una cualificacion.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> save([FromBody] QualificationSaveDTO inputDTO)
        {
            try
            {

                Qualification model = this._mapper.Map<Qualification>(inputDTO);
                QualificationDTO dto = this._mapper.Map<QualificationDTO>(await this._qualificationService.Save(model));
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
                    qualification = dto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un cualificacion.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] QualificationDTO inputDTO)
        {
            try
            {
                Qualification modelBD = await this._qualificationService.Get(id);
                if (modelBD == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }
                Qualification model = this._mapper.Map<Qualification>(inputDTO);
                modelBD.QualificationName = model.QualificationName;

                bool response = await this._qualificationService.Update(modelBD);
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
                    student = this._mapper.Map<QualificationDTO>(modelBD)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar un cualificacion.");
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
                Qualification modelDB = await this._qualificationService.Get(id);
                if (modelDB == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "NOT_FOUND",
                        message = "Not found"
                    });
                }
                bool response = await this._qualificationService.Delete(modelDB);
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
                _logger.LogError(ex, "Error al eliminar la cualificaion.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "Internal server error"
                });
            }
        }
    }
}

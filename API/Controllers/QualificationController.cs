using API.DTOs.Qualification;
using API.Models;
using API.Services.Contract;
using API.Utility;
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
        private readonly string identifier;

        public QualificationController(
            ILogger<QualificationController> logger,
            IMapper mapper,
            IQualificationService qualificationService
        )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._qualificationService = qualificationService;
            this.identifier = "xxxxxxxxxx";

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                List<QualificationDTO> dTOs = this._mapper.Map<List<QualificationDTO>>(await _qualificationService.List());
                if (dTOs.Count <= 0)
                {
                    return ResponseHandler.NotFoundResponse();
                }
                return ResponseHandler.success(this.identifier, dTOs);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de cualificacion.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }
                return ResponseHandler.success(this.identifier, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener una cualificacion.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.badRequestResponse();
                }
                return ResponseHandler.created(this.identifier, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un cualificacion.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }
                Qualification model = this._mapper.Map<Qualification>(inputDTO);
                modelBD.QualificationName = model.QualificationName;

                bool response = await this._qualificationService.Update(modelBD);
                if (!response)
                {
                    return ResponseHandler.internalServerError();
                }
                return ResponseHandler.success(identifier, this._mapper.Map<QualificationDTO>(modelBD));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar un cualificacion.");
                return ResponseHandler.internalServerError();
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
                    return ResponseHandler.NotFoundResponse();
                }
                bool response = await this._qualificationService.Delete(modelDB);
                if (!response)
                {
                    return ResponseHandler.internalServerError();
                }

                return ResponseHandler.successNotData(this.identifier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la cualificaion.");
                return ResponseHandler.internalServerError();
            }
        }
    }
}

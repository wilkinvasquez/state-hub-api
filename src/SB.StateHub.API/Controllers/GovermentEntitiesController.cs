using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SB.StateHub.API.Authorization.Attributes;
using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Results;
using SB.StateHub.API.Services.GovermentEntities;
using SB.StateHub.API.Services.Results;

namespace SB.StateHub.API.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GovermentEntitiesController : ControllerBase
    {
        private readonly IValidator<CreateOrUpdateGovermentEntityDto> _validator;
        private readonly IResultService _resultService;
        private readonly IGovermentEntityService _govermentEntityService;

        public GovermentEntitiesController(IValidator<CreateOrUpdateGovermentEntityDto> validator, IResultService resultService, IGovermentEntityService govermentEntityService)
        {
            _validator = validator;
            _resultService = resultService;
            _govermentEntityService = govermentEntityService;
        }

        // GET: api/<GovermentEntityController>
        [HttpGet]
        public ResultDto Get()
        {
            try
            {
                IEnumerable<GovermentEntityDto> govermentEntities = _govermentEntityService.GetAll();
                return _resultService.CreateSuccessResult(govermentEntities);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<GovermentEntityController>/5
        [HttpGet("{id}")]
        public async Task<ResultDto> Get(int id)
        {
            try
            {
                GovermentEntityDto govermentEntity = await _govermentEntityService.GetByIdAsync(id);
                return _resultService.CreateSuccessResult(govermentEntity);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<GovermentEntityController>
        [HttpGet("pagination")]
        public ResultDto GetAll([FromQuery] PaginationDto parameters)
        {
            try
            {
                PaginationResponseDto<GovermentEntityDto> paginationResponse = _govermentEntityService.GetAllPagedGovermentEntities(parameters);

                return _resultService.CreateSuccessResult(paginationResponse);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // POST api/<GovermentEntityController>
        [HttpPost]
        public async Task<ResultDto> Post([FromBody] CreateOrUpdateGovermentEntityDto govermentEntity)
        {
            try
            {
                var validation = _validator.Validate(govermentEntity);

                if (!validation.IsValid)
                {
                    string[] errors = validation.Errors.Select(err => err.ErrorMessage).ToArray();
                    return _resultService.CreateErrorResult(errors);
                }

                CreateOrUpdateGovermentEntityDto createdOrUpdatedGovermentEntity = await _govermentEntityService.CreateOrUpdateAsync(govermentEntity);
                return _resultService.CreateSuccessResult(createdOrUpdatedGovermentEntity);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // DELETE api/<GovermentEntityController>/5
        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            try
            {
                await _govermentEntityService.DeleteAsync(id);
                return _resultService.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }
    }
}
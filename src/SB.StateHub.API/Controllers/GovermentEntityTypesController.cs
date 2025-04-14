using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SB.StateHub.API.Authorization.Attributes;
using SB.StateHub.API.DTOs.GovermentEntityTypes;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Results;
using SB.StateHub.API.Services.GovermentEntityTypes;
using SB.StateHub.API.Services.Results;

namespace SB.StateHub.API.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GovermentEntityTypesController : ControllerBase
    {
        private readonly IValidator<CreateOrUpdateGovermentEntityTypeDto> _validator;
        private readonly IResultService _resultService;
        private readonly IGovermentEntityTypeService _govermentEntityTypeService;

        public GovermentEntityTypesController(IValidator<CreateOrUpdateGovermentEntityTypeDto> validator, IResultService resultService, IGovermentEntityTypeService govermentEntityTypeService)
        {
            _validator = validator;
            _resultService = resultService;
            _govermentEntityTypeService = govermentEntityTypeService;
        }

        // GET: api/<GovermentEntityTypesController>
        [HttpGet]
        public ResultDto Get()
        {
            try
            {
                IEnumerable<GovermentEntityTypeDto> govermentEntityTypes = _govermentEntityTypeService.GetAll<GovermentEntityTypeDto>();
                return _resultService.CreateSuccessResult(govermentEntityTypes);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<GovermentEntityTypesController>/5
        [HttpGet("{id}")]
        public async Task<ResultDto> Get(int id)
        {
            try
            {
                GovermentEntityTypeDto govermentEntityType = await _govermentEntityTypeService.GetByIdAsync<GovermentEntityTypeDto>(id);
                return _resultService.CreateSuccessResult(govermentEntityType);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<GovermentEntityTypesController>
        [HttpGet("pagination")]
        public ResultDto GetAll([FromQuery] PaginationDto parameters)
        {
            try
            {
                PaginationResponseDto<GovermentEntityTypeDto> paginationResponse = _govermentEntityTypeService.GetAllPagedGovermentEntityTypes(parameters);

                return _resultService.CreateSuccessResult(paginationResponse);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // POST api/<GovermentEntityTypesController>
        [HttpPost]
        public async Task<ResultDto> Post([FromBody] CreateOrUpdateGovermentEntityTypeDto govermentEntityType)
        {
            try
            {
                var validation = _validator.Validate(govermentEntityType);

                if (!validation.IsValid)
                {
                    string[] errors = validation.Errors.Select(err => err.ErrorMessage).ToArray();
                    return _resultService.CreateErrorResult(errors);
                }

                CreateOrUpdateGovermentEntityTypeDto createdOrUpdatedGovermentEntityType = await _govermentEntityTypeService.CreateOrUpdateAsync(govermentEntityType);
                return _resultService.CreateSuccessResult(createdOrUpdatedGovermentEntityType);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // DELETE api/<GovermentEntityTypesController>/5
        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            try
            {
                await _govermentEntityTypeService.DeleteAsync(id);
                return _resultService.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }
    }
}
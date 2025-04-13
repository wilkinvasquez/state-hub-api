using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SB.StateHub.API.DTOs.GovermentEntityTypes;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Results;
using SB.StateHub.API.Services.GovermentEntityTypes;
using SB.StateHub.API.Services.Results;

namespace SB.StateHub.API.Controllers
{
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
                IEnumerable<GovermentEntityTypeDto> hosts = _govermentEntityTypeService.GetAll<GovermentEntityTypeDto>();
                return _resultService.CreateSuccessResult(hosts);
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
                GovermentEntityTypeDto host = await _govermentEntityTypeService.GetByIdAsync<GovermentEntityTypeDto>(id);
                return _resultService.CreateSuccessResult(host);
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
                PaginationResponseDto<GovermentEntityTypeDto> hosts = _govermentEntityTypeService.GetAllPagedGovermentEntityTypes(parameters);

                return _resultService.CreateSuccessResult(hosts);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // POST api/<GovermentEntityTypesController>
        [HttpPost]
        public async Task<ResultDto> Post([FromBody] CreateOrUpdateGovermentEntityTypeDto host)
        {
            try
            {
                var validation = _validator.Validate(host);

                if (!validation.IsValid)
                {
                    string[] errors = validation.Errors.Select(err => err.ErrorMessage).ToArray();
                    return _resultService.CreateErrorResult(errors);
                }

                CreateOrUpdateGovermentEntityTypeDto createdOrUpdatedHost = await _govermentEntityTypeService.CreateOrUpdateAsync(host);
                return _resultService.CreateSuccessResult(createdOrUpdatedHost);
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
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Results;
using SB.StateHub.API.Services.Users;
using SB.StateHub.API.Services.Results;
using SB.StateHub.API.Authorization.Attributes;
using SB.StateHub.API.DTOs.Authentications;

namespace SB.StateHub.API.Controllers
{
    [ApiAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IValidator<CreateOrUpdateUserDto> _validator;
        private readonly IResultService _resultService;
        private readonly IUserService _userService;

        public UsersController(IValidator<CreateOrUpdateUserDto> validator, IResultService resultService, IUserService userService)
        {
            _validator = validator;
            _resultService = resultService;
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ResultDto Get()
        {
            try
            {
                IEnumerable<UserDto> users = _userService.GetAll<UserDto>();
                return _resultService.CreateSuccessResult(users);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ResultDto> Get(int id)
        {
            try
            {
                UserDto user = await _userService.GetByIdAsync<UserDto>(id);
                return _resultService.CreateSuccessResult(user);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // GET api/<UserController>
        [HttpGet("pagination")]
        public ResultDto GetAll([FromQuery] PaginationDto parameters)
        {
            try
            {
                PaginationResponseDto<UserDto> paginationResponse = _userService.GetAllPagedUsers(parameters);

                return _resultService.CreateSuccessResult(paginationResponse);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // POST api/<UserController>
        [ApiAnonymous]
        [HttpPost]
        public async Task<ResultDto> Post([FromBody] CreateOrUpdateUserDto user)
        {
            try
            {
                var validation = _validator.Validate(user);

                if (!validation.IsValid)
                {
                    string[] errors = validation.Errors.Select(err => err.ErrorMessage).ToArray();
                    return _resultService.CreateErrorResult(errors);
                }

                UserDto createdOrUpdatedUser = await _userService.CreateOrUpdateUserAsync(user);

                return _resultService.CreateSuccessResult(createdOrUpdatedUser);
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return _resultService.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return _resultService.CreateErrorResult(ex.Message.ToString());
            }
        }

        
        // POST api/<UsersController/authentication>
        [ApiAnonymous]
        [HttpPost("authentication/[controller]")]
        public async Task<ResultDto> Authentication(AuthenticationDto authentication)
        {
            AuthenticationResultDto result = await _userService.AuthenticateAsync(authentication);
            return _resultService.CreateSuccessResult(result);
        }
    }
}
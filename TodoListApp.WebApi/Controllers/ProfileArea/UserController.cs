namespace TodoListApp.WebApi.Controllers.ProfileArea
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Services.Services;
    using TodoListApp.WebApi.Models.Models;

    [ApiController]
    [Area("Profile")]
    [Route("api/profile/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService userService, AutoMapper.IMapper mapper)
        {
            this._logger = logger;
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            this._logger.LogInformation("GetAll Users requested");

            var users = await this._userService.GetAllAsync();
            var dto = this._mapper.Map<IEnumerable<UserDto>>(users);

            this._logger.LogInformation("Returned {Count} users", dto?.Count() ?? 0);

            return this.Ok(dto);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            this._logger.LogInformation("GetById called for user id={Id}", id);
            if (id <= 0)
            {
                this._logger.LogWarning("GetById received invalid id={Id}", id);
                return this.BadRequest("Invalid user id");
            }

            var user = await this._userService.GetById(id);
            if (user == null)
            {
                this._logger.LogWarning("User not found id={Id}", id);
                return this.NotFound();
            }

            this._logger.LogInformation("User found id={Id}", id);
            return this.Ok(this._mapper.Map<UserDto>(user));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            this._logger.LogInformation("Update called for user id={Id}", id);
            if (id <= 0 || string.IsNullOrWhiteSpace(userUpdateDto.Nickname) || userUpdateDto == null)
            {
                this._logger.LogWarning("Update received invalid data for id={Id}", id);
                return this.BadRequest("Invalid user data");
            }

            var userModel = this._mapper.Map<UserModel>(userUpdateDto);
            userModel.Id = id;

            this._logger.LogDebug("Updating user id={Id} with name={Name}", id, userModel.Nickname);

            await this._userService.Update(userModel);

            this._logger.LogInformation("User updated id={Id}", id);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            this._logger.LogInformation("Delete called for user id={Id}", id);
            if (id <= 0)
            {
                this._logger.LogWarning("Delete received invalid id={Id}", id);
                return this.BadRequest("Invalid user id");
            }

            await this._userService.Delete(id);
            this._logger.LogInformation("User deleted id={Id}", id);
            return this.NoContent();
        }
    }
}

namespace TodoListApp.WebApi.Controllers.HomeArea
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Services.Services;
    using TodoListApp.WebApi.Models.Models;

    [ApiController]
    [Area("Home")]
    [Route("api/[area]/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<GroupController> _logger;

        public GroupController(
            GroupService groupService,
            AutoMapper.IMapper mapper,
            ILogger<GroupController> logger)
        {
            this._groupService = groupService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int rowCount = 10)
        {
            this._logger.LogInformation("GetAll groups requested (page={Page}, rowCount={RowCount})", pageNumber, rowCount);

            var groups = await this._groupService.GetAllAsync(pageNumber, rowCount);
            var dto = this._mapper.Map<IEnumerable<GroupDto>>(groups);

            this._logger.LogInformation("Returned {Count} groups for page {Page}", dto?.Count() ?? 0, pageNumber);

            return this.Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            this._logger.LogInformation("GetById called for group id={Id}", id);

            if (id <= 0)
            {
                this._logger.LogWarning("GetById received invalid id={Id}", id);
                return this.BadRequest("Invalid group id");
            }

            var group = await this._groupService.GetById(id);
            if (group == null)
            {
                this._logger.LogWarning("Group not found id={Id}", id);
                return this.NotFound();
            }

            this._logger.LogInformation("Group found id={Id}", id);
            return this.Ok(this._mapper.Map<GroupDetailDto>(group));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto groupCreateForm)
        {
            this._logger.LogInformation("Create group request received");

            if (groupCreateForm == null)
            {
                this._logger.LogWarning("Create called with null body");
                return this.BadRequest("Group data is required");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                this._logger.LogWarning("Create: UserId not found in token");
                return this.Unauthorized("UserId not found in token");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Create: Invalid UserId in token value={Claim}", userIdClaim);
                return this.Unauthorized("Invalid UserId in token");
            }

            var groupModel = this._mapper.Map<GroupModel>(groupCreateForm);
            groupModel.CreatedByUserId = userId;

            this._logger.LogInformation("Creating group (name={Name}) by userId={UserId}", groupModel.Name, groupModel.CreatedByUserId);

            await this._groupService.Add(groupModel);

            this._logger.LogInformation("Group created with id={Id}", groupModel.Id);

            return this.CreatedAtAction(nameof(this.GetById), new { id = groupModel.Id }, this._mapper.Map<GroupDetailDto>(groupModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GroupUpdateDto groupUpdateForm)
        {
            this._logger.LogInformation("Update group request received for id={Id}", id);

            if (id <= 0)
            {
                this._logger.LogWarning("Update received invalid id={Id}", id);
                return this.BadRequest("Invalid group id");
            }

            if (groupUpdateForm == null)
            {
                this._logger.LogWarning("Update called with null body for id={Id}", id);
                return this.BadRequest("Group data is required");
            }

            var groupModel = this._mapper.Map<GroupModel>(groupUpdateForm);
            groupModel.Id = id;

            this._logger.LogDebug("Updating group id={Id} with name={Name}", id, groupModel.Name);

            await this._groupService.Update(groupModel);

            this._logger.LogInformation("Group updated id={Id}", id);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            this._logger.LogInformation("Delete group request received for id={Id}", id);

            if (id <= 0)
            {
                this._logger.LogWarning("Delete received invalid id={Id}", id);
                return this.BadRequest("Invalid group id");
            }

            await this._groupService.Delete(id);

            this._logger.LogInformation("Group deleted id={Id}", id);

            return this.NoContent();
        }
    }
}

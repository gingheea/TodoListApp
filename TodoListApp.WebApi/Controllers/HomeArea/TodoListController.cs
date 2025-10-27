#pragma warning disable SA1101
namespace TodoListApp.WebApi.Controllers.HomeArea
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Services.Services;
    using TodoListApp.WebApi.Models.Models;

    [Area("Home")]
    [Route("api/[area]/todolist")]
    [Route("api/[area]/group/{groupId}/todolist")]
    [ApiController]
#pragma warning disable CA1848
    public class TodoListController : ControllerBase
    {
        private readonly TodoListService _todoListService;
        private readonly ILogger<TodoListController> _logger;
        private readonly AutoMapper.IMapper _mapper;

        public TodoListController(
            TodoListService todoListService,
            AutoMapper.IMapper mapper,
            ILogger<TodoListController> logger)
        {
            this._todoListService = todoListService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdmin(int pageNumber = 1, int rowCount = 10)
        {
            this._logger.LogInformation("GetAll todo lists requested (page={Page}, rowCount={RowCount})", pageNumber, rowCount);

            var todoItems = await this._todoListService.GetAllAsync(pageNumber, rowCount);
            var dto = this._mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            this._logger.LogInformation("Returned {Count} todo items for page {Page}", dto?.Count() ?? 0, pageNumber);

            return this.Ok(dto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllForUser([FromQuery] int? groupId, int page = 1, int size = 10)
        {
            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return this.Unauthorized("Invalid UserId");
            }

            var lists = await this._todoListService.GetAllAsync(userId, groupId, page, size);
            return this.Ok(this._mapper.Map<IEnumerable<TodoListDto>>(lists));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            this._logger.LogInformation("GetById called for todoList id={Id}", id);

            if (id <= 0)
            {
                return this.BadRequest("Invalid id");
            }

            var todoList = await this._todoListService.GetById(id);

            if (todoList == null || todoList.Id != id)
            {
                return this.NotFound();
            }

            return this.Ok(this._mapper.Map<TodoListDetailDto>(todoList));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromQuery] int? groupId, [FromBody] TodoListCreateDto todoListCreateForm)
        {
            this._logger.LogInformation("Create todo list request received");

            if (todoListCreateForm == null)
            {
                this._logger.LogWarning("Create called with null body");
                return this.BadRequest("Todo list data is required");
            }

            if (string.IsNullOrWhiteSpace(todoListCreateForm.Title))
            {
                return this.BadRequest("Invalid todo list data");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Create: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            var todoListModel = this._mapper.Map<TodoListModel>(todoListCreateForm);
            todoListModel.GroupId = groupId > 0 ? groupId : null;

            await this._todoListService.Add(todoListModel, userId);
            return this.CreatedAtAction(nameof(this.GetById), new { id = todoListModel.Id }, todoListModel);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] TodoListUpdateDto todoListUpdateForm)
        {
            this._logger.LogInformation("Update todo list request received for id={Id}", id);

            if (todoListUpdateForm == null)
            {
                this._logger.LogWarning("Update called with null body");
                return this.BadRequest("Todo list data is required");
            }

            if (string.IsNullOrWhiteSpace(todoListUpdateForm.Title))
            {
                return this.BadRequest("Invalid todo list data");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Update: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            var existing = await this._todoListService.GetById(id);
            if (existing == null)
            {
                return this.NotFound();
            }

            var role = await this._todoListService.GetUserRoleInListAsync(userId, id);
            if (role is not TodoListRole.Owner and not TodoListRole.Editor)
            {
                this._logger.LogWarning("Update denied: user {UserId} has insufficient rights for list {ListId}", userId, id);
                return this.Forbid();
            }

            var todoListModel = this._mapper.Map<TodoListModel>(todoListUpdateForm);
            todoListModel.Id = id;

            await this._todoListService.Update(todoListModel);

            this._logger.LogInformation("Todo list with id={Id} updated successfully", id);

            return this.Ok(this._mapper.Map<TodoListDetailDto>(todoListModel));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            this._logger.LogInformation("Delete todo list request received for id={Id}", id);

            if (id <= 0)
            {
                return this.BadRequest("Invalid id");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Delete: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            // Перевірка ролі користувача
            var role = await this._todoListService.GetUserRoleInListAsync(userId, id);
            if (role is not TodoListRole.Owner and not TodoListRole.Editor)
            {
                this._logger.LogWarning("Delete: User {UserId} has no permission to delete  list {ListId}", userId, id);
                return this.Forbid();
            }

            await this._todoListService.Delete(id);

            this._logger.LogInformation("Todo list with id={Id} deleted successfully", id);

            return this.NoContent();
        }
#pragma warning restore CA1848
    }
}

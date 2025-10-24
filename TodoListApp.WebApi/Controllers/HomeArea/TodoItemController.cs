namespace TodoListApp.WebApi.Controllers.HomeArea
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Entities.Entities;
    using TodoListApp.Services.Services;
    using TodoListApp.WebApi.Models.Models;

    [Area("Home")]
    [Route("api/[area]/todolist/{listId:int}/todoitems")]
    [ApiController]
#pragma warning disable CA1848
    public class TodoItemController : ControllerBase
    {
        private readonly TodoItemService _todoItemService;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(
            TodoItemService todoItemService,
            AutoMapper.IMapper mapper,
            ILogger<TodoItemController> logger)
        {
            this._todoItemService = todoItemService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdmin([FromQuery] int pageNumber = 1, [FromQuery] int rowCount = 10)
        {
            this._logger.LogInformation("GetAll todo items requested (page={Page}, rowCount={RowCount})", pageNumber, rowCount);

            var todoItems = await this._todoItemService.GetAllAsync(pageNumber, rowCount);
            var dto = this._mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            this._logger.LogInformation("Returned {Count} todo items for page {Page}", dto?.Count() ?? 0, pageNumber);

            return this.Ok(dto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllForUser(int listId, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            this._logger.LogInformation("GetAll todo items requested (page={Page}, rowCount={RowCount})", page, size);

            var todoItems = await this._todoItemService.GetAllAsync(listId, page, size);
            var dto = this._mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            this._logger.LogInformation("Returned {Count} todo items for page {Page}", dto?.Count() ?? 0, page);

            return this.Ok(dto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int listId, int id)
        {
            this._logger.LogInformation("GetById called for todoItem id={Id} in listId={ListId}", id, listId);

            if (id <= 0 || listId <= 0)
            {
                return this.BadRequest("Invalid id");
            }

            var todoItem = await this._todoItemService.GetById(id);

            if (todoItem == null || todoItem.TodoListId != listId)
            {
                return this.NotFound();
            }

            return this.Ok(this._mapper.Map<TodoItemDto>(todoItem));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int listId, [FromBody] TodoItemCreateDto todoItemCreateForm)
        {
            this._logger.LogInformation("Create todo item request received for listId={ListId}", listId);

            if (todoItemCreateForm == null)
            {
                this._logger.LogWarning("Create called with null body");
                return this.BadRequest("Todo item data is required");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Create: Invalid or missing UserId in token");
                return this.Unauthorized("Invalid user");
            }

            // 🔑 Перевірка ролі користувача у списку
            var userRole = await this._todoItemService.GetUserRoleInListAsync(userId, listId);

            if (userRole == null)
            {
                this._logger.LogWarning("Create: userId={UserId} does not belong to listId={ListId}", userId, listId);
                return this.Forbid("User not part of this list");
            }

            if (userRole == TodoListRole.Viewer)
            {
                this._logger.LogWarning("Create: userId={UserId} with role={Role} tried to create item in listId={ListId}", userId, userRole, listId);
                return this.Forbid("Viewers cannot create todo items");
            }

            if (this.User.IsInRole("Admin"))
            {
                this._logger.LogInformation("AdminUser override for userId={UserId}", userId);
            }

            var todoItemModel = this._mapper.Map<TodoItemModel>(todoItemCreateForm);
            todoItemModel.TodoListId = listId;

            await this._todoItemService.Add(todoItemModel);

            this._logger.LogInformation("Todo item created with id={Id} by userId={UserId} in listId={ListId}", todoItemModel.Id, userId, listId);

            return this.CreatedAtAction(nameof(this.GetById), new { listId, id = todoItemModel.Id }, this._mapper.Map<TodoItemDto>(todoItemModel));
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int listId, int id, [FromBody] TodoItemUpdateDto todoItemUpdateForm)
        {
            this._logger.LogInformation("Update todo item request received for id={Id}, listId={ListId}", id, listId);

            if (todoItemUpdateForm == null)
            {
                this._logger.LogWarning("Update called with null body");
                return this.BadRequest("Todo item data is required");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Update: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            // Перевірка ролі користувача
            var role = await this._todoItemService.GetUserRoleInListAsync(userId, listId);
            if (role is not TodoListRole.Owner and not TodoListRole.Editor)
            {
                this._logger.LogWarning("Update: User {UserId} has no permission to update items in list {ListId}", userId, listId);
                return this.Forbid();
            }

            var todoItemModel = this._mapper.Map<TodoItemModel>(todoItemUpdateForm);
            todoItemModel.Id = id;
            todoItemModel.TodoListId = listId;

            await this._todoItemService.Update(todoItemModel);

            this._logger.LogInformation("Todo item with id={Id} updated successfully", id);

            return this.Ok(this._mapper.Map<TodoItemDto>(todoItemModel));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int listId, int id)
        {
            this._logger.LogInformation("Delete todo item request received for id={Id}, listId={ListId}", id, listId);

            if (id <= 0 || listId <= 0)
            {
                return this.BadRequest("Invalid id or listId");
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Delete: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            // Перевірка ролі користувача
            var role = await this._todoItemService.GetUserRoleInListAsync(userId, listId);
            if (role is not TodoListRole.Owner and not TodoListRole.Editor)
            {
                this._logger.LogWarning("Delete: User {UserId} has no permission to delete items in list {ListId}", userId, listId);
                return this.Forbid();
            }

            await this._todoItemService.Delete(id);

            this._logger.LogInformation("Todo item with id={Id} deleted successfully", id);

            return this.NoContent();
        }

        [HttpPut("{id}/status")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(int listId, int id, [FromBody] ChangeStatusDto dto)
        {
            this._logger.LogInformation("ChangeStatus called for todoItem id={Id} in listId={ListId}", id, listId);
            if (id <= 0 || listId <= 0)
            {
                return this.BadRequest("Invalid id or listId");
            }

            var todoItem = await this._todoItemService.GetById(id);
            if (todoItem == null || todoItem.TodoListId != listId)
            {
                return this.NotFound();
            }

            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                this._logger.LogWarning("Delete: Invalid or missing userId claim");
                return this.Unauthorized("Invalid UserId");
            }

            var role = await this._todoItemService.GetUserRoleInListAsync(userId, listId);
            if (role is not TodoListRole.Owner and not TodoListRole.Editor)
            {
                this._logger.LogWarning("Delete: User {UserId} has no permission to delete items in list {ListId}", userId, listId);
                return this.Forbid();
            }

            await this._todoItemService.ToggleCompleteAsync(id, dto.IsCompleted);

            this._logger.LogInformation("Todo item with id={Id} status changed to IsCompleted={IsCompleted}", id, dto.IsCompleted);

            return this.NoContent();
        }
#pragma warning restore CA1848
    }
}

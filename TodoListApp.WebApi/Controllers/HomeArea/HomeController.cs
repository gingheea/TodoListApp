#pragma warning disable SA1649
namespace TodoListApp.WebApi.Controllers.HomeArea
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Services.Services;

    [ApiController]
    [Area("Home")]
    [Route("api/[area]/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly TaskService _taskService;

        public DashboardController(TaskService taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDto>> GetDashboardInfo()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = this.User.FindFirst("Nickname")?.Value ?? "User";
            var role = this.User.FindFirstValue(ClaimTypes.Role) ?? "Standard";

            _ = int.TryParse(userId, out int userIdInt);

            var tasks = await this._taskService.GetUserTasksAsync(userIdInt);

            var total = tasks.Count();
            var completed = tasks.Count(t => t.IsCompleted);
            var due = tasks.Count(t => t.DueDate < DateTime.UtcNow && !t.IsCompleted);

            var dto = new DashboardDto
            {
                UserName = userName,
                Role = role,
                TasksDue = due,
                TotalCount = total,
                CompletionPercent = total == 0 ? 0 : (double)completed / total * 100,
            };

            return this.Ok(dto);
        }
    }
}

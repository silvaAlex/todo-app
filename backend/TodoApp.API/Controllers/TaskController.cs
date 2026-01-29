using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.UseCases;

namespace TodoApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(ITaskService taskService, DomainNotifier notifier) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;
        private readonly DomainNotifier _notifier = notifier;

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest taskCreated)
        {
            var userId = GetUserId();

            var result = await _taskService.CreateTaskAsync(new TaskCreatedDto(taskCreated.Title, taskCreated.Description, taskCreated.Category, userId));

            if (_notifier.HasNotifications)
                return BadRequest(_notifier.Notifications);

            return CreatedAtAction(nameof(GetTaskById), new { id = result!.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByUser()
        {
            var userId = GetUserId();

            var result = await _taskService.GetTasksByUserAsync(userId);

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _taskService.GetTaskByIdAsync(id);

            if (_notifier.HasNotifications)
                return NotFound(_notifier.Notifications);

            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateDto task)
        {
            var result = await _taskService.UpdateTaskAsync(task, id);

            if (_notifier.HasNotifications)
                return BadRequest(_notifier.Notifications);

            return Ok(result);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Complete(Guid id)
        {
            await _taskService.CompleteTaskAsync(id);

            if (_notifier.HasNotifications)
                return BadRequest(_notifier.Notifications);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);

            if (_notifier.HasNotifications)
                return BadRequest(_notifier.Notifications);

            return NoContent();
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetTasksByCategory(string category)
        {
            var userId = GetUserId();

            var result = await _taskService.GetTasksByCategoryAsync(category, userId);

            return Ok(result);
        }
    }
}

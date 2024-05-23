
using aspnetcore.ntier.BLL.Services;
using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.API.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;
        private readonly ILogger<TaskService> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskService> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }


        [HttpGet("gettasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await _taskService.GetTasksAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("addtask")]
        public async Task<IActionResult> AddTask(TaskToAddDTO taskToAddDTO)
        {
            try
            {
                return Ok(await _taskService.AddTaskAsync(taskToAddDTO));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }


        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatusTask(int taskId)
        {
            try
            {
                return Ok(await _taskService.UpdateStatusTaskAsync(taskId));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("updatetask")]
        public async Task<IActionResult> UpdateTask(TaskDTO taskToUpdate)
        {
            try
            {
                return Ok(await _taskService.UpdateTaskAsync(taskToUpdate));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpDelete("deletetask")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _taskService.DeleteTaskAsync(taskId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}

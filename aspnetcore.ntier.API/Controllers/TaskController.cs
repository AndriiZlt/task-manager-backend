
using aspnetcore.ntier.BLL.Services;
using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.API.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]

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
        /*        public async Task<IActionResult> GetTasks()
                {
                    try
                    {
                        _logger.LogInformation("Controller start");
                        var result = await _taskService.GetTasksAsync();
                        _logger.LogInformation("Incontroller result {Count}", result.ToArray()[0]);
                        return Ok(result);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Something went wrong");
                    }
                }*/

        public async Task<List<Taskk>> GetTasks()
        {
                _logger.LogInformation("Controller start");
                var result = await _taskService.GetTasksAsync();
                _logger.LogInformation("Incontroller result {Count}", result.ToArray()[0]);
            return result;
        }
    }
}

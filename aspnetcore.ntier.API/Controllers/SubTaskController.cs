
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

    public class SubtaskController : ControllerBase
    {

        private readonly ISubtaskService _subtaskService;
        private readonly ILogger<SubtaskService> _logger;

        public SubtaskController(ISubtaskService subtaskService, ILogger<SubtaskService> logger)
        {
            _subtaskService = subtaskService;
            _logger = logger;
        }

        [HttpGet("getsubtasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await _subtaskService.GetSubtasksAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("addsubtask")]
        public async Task<IActionResult> AddTask(SubtaskToAddDTO taskToAddDTO)
        {
            try
            {
                return Ok(await _subtaskService.AddSubtaskAsync(taskToAddDTO));
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
                return Ok(await _subtaskService.UpdateStatusSubtaskAsync(taskId));
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

        [HttpPut("updatesubtask")]
        public async Task<IActionResult> UpdateTask(SubtaskDTO taskToUpdate)
        {
            try
            {
                return Ok(await _subtaskService.UpdateSubtaskAsync(taskToUpdate));
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

        [HttpDelete("deletesubtask")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _subtaskService.DeleteSubtaskAsync(taskId);
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

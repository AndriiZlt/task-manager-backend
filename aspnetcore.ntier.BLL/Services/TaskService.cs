using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;



namespace aspnetcore.ntier.BLL.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHubContext<SignalHub> _signalrContext;

        public TaskService (ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger, IHttpContextAccessor httpContext, IHubContext<SignalHub> signalrContext)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContext = httpContext;
            _signalrContext = signalrContext;
        }

        public async Task<List<TaskDTO>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasksToReturn = await _taskRepository.GetListAsync(Int32.Parse(userId));
            return _mapper.Map<List<TaskDTO>>(tasksToReturn);
        }

        public async Task<TaskDTO> AddTaskAsync([FromBody] TaskToAddDTO taskToAddDTO)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            taskToAddDTO.UserId = Convert.ToInt32(userId);

            var addedTask = await _taskRepository.AddAsync(_mapper.Map<Taskk>(taskToAddDTO));
            return _mapper.Map<TaskDTO>(addedTask);
        }

 

        public async Task DeleteTaskAsync(int taskId)
        {
            var taskToDelete = await _taskRepository.GetAsync(x => x.Id == taskId);

            if (taskToDelete is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            await _taskRepository.DeleteAsync(taskToDelete);
        }

        public async Task<TaskDTO> UpdateStatusTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetAsync(x => x.Id == taskId);
            if (task is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            string status = task.Status == "undone" ? "completed" : "undone";

            task.Status = status;

            var taskToUpdate = _mapper.Map<Taskk>(task);

            _logger.LogInformation("Task with these properties: {@TaskToUpdate} has been updated", task);

            return _mapper.Map<TaskDTO>(await _taskRepository.UpdateStatusTaskAsync(taskToUpdate));
        }

        public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskToUpdate)
        {
            var taskBeforeUpdate = await _taskRepository.GetAsync(x => x.Id == taskToUpdate.Id);

            if (taskBeforeUpdate is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskToUpdate.Id);
                throw new KeyNotFoundException();
            }

            taskBeforeUpdate.Title = taskToUpdate.Title;
            taskBeforeUpdate.Description = taskToUpdate.Description;
            taskBeforeUpdate.Status = taskToUpdate.Status;
            taskBeforeUpdate.UserId= (int)taskToUpdate.UserId;
            taskBeforeUpdate.DateCompleted = taskToUpdate.DateCompleted;

            var taskAfterUpdate = _mapper.Map<Taskk>(taskBeforeUpdate);

            _logger.LogInformation("Task with these properties: {@TaskToUpdate} has been updated", taskAfterUpdate);

            return _mapper.Map<TaskDTO>(await _taskRepository.UpdateTaskAsync(taskAfterUpdate));
        }

        private void sendNotification(int userId, string message)
        {
            _signalrContext.Clients.All.SendAsync("test");
        }

    }
}


/*            _logger.LogInformation("Service result {Count}", tasksToReturn.ToArray()[0]);*/
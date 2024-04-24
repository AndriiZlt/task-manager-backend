using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;



namespace aspnetcore.ntier.BLL.Services
{
    public class SubtaskService : ISubtaskService
    {

        private readonly ISubtaskRepository _subtaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SubtaskService> _logger;


        public SubtaskService (ISubtaskRepository subtaskRepository, IMapper mapper, ILogger<SubtaskService> logger)
        {
            _subtaskRepository = subtaskRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<SubtaskDTO>> GetSubtasksAsync(CancellationToken cancellationToken = default)
        {
            var subtasksToReturn = await _subtaskRepository.GetListAsync();
            return _mapper.Map<List<SubtaskDTO>>(subtasksToReturn);
        }

        public async Task<SubtaskDTO> AddSubtaskAsync(SubtaskToAddDTO subtaskToAddDTO)
        {
            var addedSubtask = await _subtaskRepository.AddAsync(_mapper.Map<Subtask>(subtaskToAddDTO));
            return _mapper.Map<SubtaskDTO>(addedSubtask);
        }

        public async Task DeleteSubtaskAsync(int taskId)
        {
            var taskToDelete = await _subtaskRepository.GetAsync(x => x.Id == taskId);

            if (taskToDelete is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            await _subtaskRepository.DeleteAsync(taskToDelete);
        }

        public async Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId)
        {
            var task = await _subtaskRepository.GetAsync(x => x.Id == taskId);
            if (task is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            string status = task.Status == "undone" ? "completed" : "undone";

            task.Status = status;

            var taskToUpdate = _mapper.Map<Subtask>(task);

            _logger.LogInformation("Task with these properties: {@TaskToUpdate} has been updated", task);

            return _mapper.Map<SubtaskDTO>(await _subtaskRepository.UpdateStatusTaskAsync(taskToUpdate));
        }

        public async Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate)
        {
            var taskBeforeUpdate = await _subtaskRepository.GetAsync(x => x.Id == taskToUpdate.Id);

            if (taskBeforeUpdate is null)
            {
                _logger.LogError("Task with taskId = {TaskId} was not found", taskToUpdate.Id);
                throw new KeyNotFoundException();
            }

            taskBeforeUpdate.Title = taskToUpdate.Title;
            taskBeforeUpdate.Description = taskToUpdate.Description;
            taskBeforeUpdate.Status = taskToUpdate.Status;
            taskBeforeUpdate.TaskId = taskToUpdate.TaskId;
            taskBeforeUpdate.DateCompleted = taskToUpdate.DateCompleted;

            var taskAfterUpdate = _mapper.Map<Subtask>(taskBeforeUpdate);

            _logger.LogInformation("Task with these properties: {@TaskToUpdate} has been updated", taskAfterUpdate);

            return _mapper.Map<SubtaskDTO>(await _subtaskRepository.UpdateTaskAsync(taskAfterUpdate));
        }

    }
}


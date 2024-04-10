using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;



namespace aspnetcore.ntier.BLL.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;


        public TaskService (ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _logger = logger;

        }

/*        public async Task<List<TaskDTO>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            var tasksToReturn = await _taskRepository.GetListAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("Tasks returned {Count}", tasksToReturn.Count);

            return _mapper.Map<List<TaskDTO>>(tasksToReturn);
        }*/

        public async Task<List<Taskk>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            var tasksToReturn = await _taskRepository.GetListAsync();
            _logger.LogInformation("Tasks returned {Count}", tasksToReturn.ToArray()[0].Description);

            return _mapper.Map<List<Taskk>>(tasksToReturn);
        }

    }
}

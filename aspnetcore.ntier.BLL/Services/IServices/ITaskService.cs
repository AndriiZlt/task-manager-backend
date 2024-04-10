
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface ITaskService
{
    Task<List<Taskk>> GetTasksAsync(CancellationToken cancellationToken = default);
}

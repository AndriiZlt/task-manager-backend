using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface ITaskRepository 
    {

        Task<List<Taskk>> GetListAsync();
    }
}
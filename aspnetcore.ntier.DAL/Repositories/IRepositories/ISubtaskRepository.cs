using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface ISubtaskRepository:IGenericRepository<Subtask>
    {
/*        Task<SubTask> GetAsync(Expression<Func<SubTask, bool>> filter = null, CancellationToken cancellationToken = default);

        Task<List<SubTask>> GetListAsync();

        Task<SubTask> AddAsync(SubTask task);

        Task<int> DeleteAsync(SubTask task);*/

        Task<Subtask> UpdateStatusTaskAsync(Subtask task);

        Task<Subtask> UpdateTaskAsync(Subtask task);

    }
}
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class SubtaskRepository : GenericRepository<Subtask>, ISubtaskRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public SubtaskRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) : base(aspNetCoreNTierDbContext)
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        /*public async Task<List<SubTask>> GetListAsync()
        {
            return await _aspNetCoreNTierDbContext.Set<SubTask>().ToListAsync();
        }

        public async Task<SubTask> GetAsync(Expression<Func<SubTask, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<SubTask>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<SubTask> AddAsync(SubTask task)
        {
            await _aspNetCoreNTierDbContext.AddAsync(task);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<int> DeleteAsync(SubTask task)
        {
            _ = _aspNetCoreNTierDbContext.Remove(task);
            return await _aspNetCoreNTierDbContext.SaveChangesAsync();
        }*/

        public async Task<Subtask> UpdateStatusTaskAsync(Subtask task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Subtask> UpdateTaskAsync(Subtask task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

    }
}


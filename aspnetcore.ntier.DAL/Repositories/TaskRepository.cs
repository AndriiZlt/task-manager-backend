using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace aspnetcore.ntier.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public TaskRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<Taskk>> GetListAsync()
        {
            return await _aspNetCoreNTierDbContext.Set<Taskk>().ToListAsync();
        }
        /*        public List<Taskk> GetTasksDAL()
                {
                    return new List<Taskk>();
                }*/

    }
}

using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using System.Collections.Generic;

namespace DemoApp.Core.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> getAllTask(int? pageIndex, int? pageSize, string search);
    }
}

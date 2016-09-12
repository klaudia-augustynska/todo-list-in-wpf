using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DAL.Repositories;

namespace TodoList.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskItemRepository Tasks { get; }
        Task<int> Complete();
    }
}

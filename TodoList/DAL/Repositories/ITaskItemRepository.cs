using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.DAL.Repositories
{
    public interface ITaskItemRepository : IRepository<Task>
    {
    }
}

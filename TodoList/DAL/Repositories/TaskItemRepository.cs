using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.DAL.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(DbContext context) 
            : base(context)
        {
        }

        public TodoListContext TaskListContext => Context as TodoListContext;
    }
}

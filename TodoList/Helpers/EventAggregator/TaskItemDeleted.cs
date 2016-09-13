using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Helpers.EventAggregator
{
    public class TaskItemDeleted
    {
        public TaskItem Task { get; private set; }

        public TaskItemDeleted(TaskItem task)
        {
            Task = task;
        }
    }
}

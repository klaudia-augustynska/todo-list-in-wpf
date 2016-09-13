using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Helpers.EventAggregator
{
    public class TaskItemUpdated
    {
        public TaskItem Task { get; private set; }

        public TaskItemUpdated(TaskItem task)
        {
            Task = task;
        }
    }
}

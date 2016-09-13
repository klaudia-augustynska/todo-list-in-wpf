using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Helpers.EventAggregator
{
    public class EnableUserToEditTask
    {
        public TaskItem Task { get; private set; }

        public EnableUserToEditTask(TaskItem task)
        {
            Task = task;
        }
    }
}

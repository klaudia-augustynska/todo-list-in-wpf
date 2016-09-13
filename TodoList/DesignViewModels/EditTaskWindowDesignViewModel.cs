using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.DesignViewModels
{
    class EditTaskWindowDesignViewModel
    {
        public TaskItem Task { get; set; }

        public EditTaskWindowDesignViewModel()
        {
            Task = GetTask();
        }

        private TaskItem GetTask()
        {
            return new TaskItem
            {
                Completed = true,
                DueToDate = DateTime.Now.AddDays(2),
                Name = "Test task"
            };
        }
    }
}

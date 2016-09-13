using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public class EditTaskWindowViewModel : ViewModelBase
    {
        public TaskItem Task { get; set; }

        public EditTaskWindowViewModel(TaskItem item)
        {
            Task = item;
        }
    }
}

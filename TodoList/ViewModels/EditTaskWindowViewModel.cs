using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class EditTaskWindowViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        public TaskItem Todo { get; set; }

        public EditTaskWindowViewModel(TaskItem item, IEventAggregator eventAggregator)
        {
            Todo = (TaskItem) item.Clone();
            _eventAggregator = eventAggregator;
            InitializeCommands();
        }
    }
}

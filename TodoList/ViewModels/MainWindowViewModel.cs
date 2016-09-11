using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _taskName;

        public ObservableCollection<TaskItem> OldTasks { get; set; }
        public ObservableCollection<TaskItem> CurrentTasks { get; set; }
        public ObservableCollection<TaskItem> FollowingTasks { get; set; }

        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; NotifyPropertyChanged(nameof(TaskName)); }
        }

        public MainWindowViewModel()
        {
            OldTasks = new ObservableCollection<TaskItem>();
            CurrentTasks = new ObservableCollection<TaskItem>();
            FollowingTasks = new ObservableCollection<TaskItem>();
        }
    }
}

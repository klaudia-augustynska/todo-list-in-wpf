using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Task> OldTasks { get; set; }
        public ObservableCollection<Task> CurrentTasks { get; set; }
        public ObservableCollection<Task> FollowingTasks { get; set; }

        public MainWindowViewModel()
        {
            OldTasks = new ObservableCollection<Task>();
            CurrentTasks = new ObservableCollection<Task>();
            FollowingTasks = new ObservableCollection<Task>();
        }
    }
}

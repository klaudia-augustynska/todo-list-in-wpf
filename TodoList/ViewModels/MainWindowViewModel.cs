using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TodoList.Helpers;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ISubscriber<TaskItemAdded>
    {
        private string _taskName;
        private IEventAggregator EventAggregator { get; set; }

        public ObservableCollection<TaskItem> OldTasks { get; set; }
        public ObservableCollection<TaskItem> CurrentTasks { get; set; }
        public ObservableCollection<TaskItem> FollowingTasks { get; set; }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                NotifyPropertyChanged(nameof(TaskName));
            }
        }

        public DateTime TaskDueToDate { get; set; }

        public MainWindowViewModel()
        {
            OldTasks = new ObservableCollection<TaskItem>();
            CurrentTasks = new ObservableCollection<TaskItem>();
            FollowingTasks = new ObservableCollection<TaskItem>();
            OldTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(OldTasks));
            CurrentTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(CurrentTasks));
            FollowingTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(FollowingTasks));
            TaskDueToDate = DateTime.Now;
            InitializeCommands();
            EventAggregator = new SimpleEventAggregator();
            EventAggregator.Subscribe(this);
        }

        public void OnEvent(TaskItemAdded e)
        {
            PutEventToList(e.Task);
        }

        private void PutEventToList(TaskItem task)
        {
            if (task.DueToDate.Date == TaskDueToDate.Date)
                CurrentTasks.Add(task);
            else if (task.DueToDate.Date < TaskDueToDate.Date)
                OldTasks.Add(task);
            else if (IsInThisWeek(task.DueToDate))
                FollowingTasks.Add(task);
        }

        private bool IsInThisWeek(DateTime date)
        {
            int daysToLastDayOfWeek = 7 - (int)TaskDueToDate.DayOfWeek;
            DateTime endOfTheWeek = TaskDueToDate.AddDays(daysToLastDayOfWeek);
            if (date.Date > TaskDueToDate.Date && date.Date <= endOfTheWeek.Date)
                return true;
            return false;
        }
    }
}

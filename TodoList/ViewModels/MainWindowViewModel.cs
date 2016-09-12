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
        private IEventAggregator EventAggregator { get; set; }

        private string _taskName;
        private DateTime _selectedDate;
        private ObservableCollection<TaskItem> _currentTasks, _oldTasks, _followingTasks;
        private bool _loading;

        public ObservableCollection<TaskItem> OldTasks
        {
            get { return _oldTasks; }
            set
            {
                _oldTasks = value;
                NotifyPropertyChanged(nameof(OldTasks));
            }
        }
        public ObservableCollection<TaskItem> CurrentTasks
        {
            get { return _currentTasks; }
            set
            {
                _currentTasks = value;
                NotifyPropertyChanged(nameof(CurrentTasks));
            }
        }
        public ObservableCollection<TaskItem> FollowingTasks
        {
            get { return _followingTasks; }
            set
            {
                _followingTasks = value;
                NotifyPropertyChanged(nameof(FollowingTasks));
            }
        }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                NotifyPropertyChanged(nameof(TaskName));
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                NotifyPropertyChanged(nameof(SelectedDate));
            }
        }

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                NotifyPropertyChanged(nameof(Loading));
            }
        }

        public MainWindowViewModel()
        {
            InitializeLists();
            InitializeProperties();
            InitializeEventAggregator();
            InitializeCommands();
        }

        private void InitializeProperties()
        {
            TaskName = string.Empty;
            SelectedDate = DateTime.Now;
            Loading = true;
        }

        private void InitializeEventAggregator()
        {
            EventAggregator = new SimpleEventAggregator();
            EventAggregator.Subscribe(this);
        }

        private void InitializeLists()
        {
            OldTasks = new ObservableCollection<TaskItem>();
            CurrentTasks = new ObservableCollection<TaskItem>();
            FollowingTasks = new ObservableCollection<TaskItem>();
            OldTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(OldTasks));
            CurrentTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(CurrentTasks));
            FollowingTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(FollowingTasks));
        }

        public void OnEvent(TaskItemAdded e)
        {
            PutEventToList(e.Task);
        }

        private void PutEventToList(TaskItem task)
        {
            if (FindCurrentTasks(task))
                CurrentTasks.Insert(0,task);
            else if (FindOldTasks(task))
                OldTasks.Insert(0, task);
            else if (FindFollowingTasks(task))
                FollowingTasks.Insert(0, task);
        }
    }
}

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
    public partial class MainWindowViewModel : ViewModelBase, 
        ISubscriber<TaskItemAdded>, ISubscriber<TaskItemUpdated>, ISubscriber<TaskItemDeleted>
    {
        private IEventAggregator _eventAggregator { get; set; }
        private Dictionary<ListType, ObservableCollection<TaskItem>> _typesToListDictionary;

        private string _taskName;
        private DateTime _selectedDate;
        private ObservableCollection<TaskItem> _currentTasks, _oldTasks, _followingTasks;
        private bool _loading, _showCompleted;

        public ObservableCollection<TaskItem> OldTasks
        {
            get { return _oldTasks; }
            set
            {
                _oldTasks = value;
                _oldTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(OldTasks));
                _typesToListDictionary[ListType.OldTasks] = _oldTasks;
                NotifyPropertyChanged(nameof(OldTasks));
            }
        }
        public ObservableCollection<TaskItem> CurrentTasks
        {
            get { return _currentTasks; }
            set
            {
                _currentTasks = value;
                _currentTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(CurrentTasks));
                _typesToListDictionary[ListType.CurrentTasks] = _currentTasks;
                NotifyPropertyChanged(nameof(CurrentTasks));
            }
        }
        public ObservableCollection<TaskItem> FollowingTasks
        {
            get { return _followingTasks; }
            set
            {
                _followingTasks = value;
                _followingTasks.CollectionChanged += (sender, args) => NotifyPropertyChanged(nameof(FollowingTasks));
                _typesToListDictionary[ListType.FollowingTasks] = _followingTasks;
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

        public bool ShowCompleted
        {
            get { return _showCompleted; }
            set
            {
                _showCompleted = value;
                NotifyPropertyChanged(nameof(ShowCompleted));
            }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            InitializeLists();
            InitializeProperties();
            InitializeCommands();
        }

        private Dictionary<ListType, ObservableCollection<TaskItem>> GetTypesToListDictionary()
        {
            return new Dictionary<ListType, ObservableCollection<TaskItem>>
            {
                { ListType.OldTasks, _oldTasks },
                { ListType.CurrentTasks, _currentTasks },
                { ListType.FollowingTasks, _followingTasks },
                { ListType.Undefined, null }
            };
        }

        private void InitializeProperties()
        {
            TaskName = string.Empty;
            SelectedDate = DateTime.Now;
            Loading = true;
            ShowCompleted = false;
        }

        private void InitializeLists()
        {
            _typesToListDictionary = GetTypesToListDictionary();
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
            var type = FindTypeOfList(task);
            switch (type)
            {
                case ListType.OldTasks:
                    OldTasks.Insert(0, task);
                    break;
                case ListType.CurrentTasks:
                    CurrentTasks.Insert(0, task);
                    break;
                case ListType.FollowingTasks:
                    FollowingTasks.Insert(0, task);
                    break;
            }
        }

        private ListType FindTypeOfList(TaskItem task)
        {
            if (FindCurrentTasks(task))
                return ListType.CurrentTasks;
            else if (FindOldTasks(task))
                return ListType.OldTasks;
            else if (FindFollowingTasks(task))
                return ListType.FollowingTasks;
            return ListType.Undefined;
        }

        private ListType LocalizeOnList(TaskItem task)
        {
            if (OldTasks.SingleOrDefault(x => x.ID == task.ID) != default (TaskItem))
                return ListType.OldTasks;
            if (CurrentTasks.SingleOrDefault(x => x.ID == task.ID) != default(TaskItem))
                return ListType.CurrentTasks;
            if (FollowingTasks.SingleOrDefault(x => x.ID == task.ID) != default(TaskItem))
                return ListType.FollowingTasks;
            return ListType.Undefined;
        }

        public void OnEvent(TaskItemUpdated e)
        {
            var currentList = LocalizeOnList(e.Task);
            var desiredList = FindTypeOfList(e.Task);
            if (currentList != desiredList)
            {
                MoveToProperList(currentList, e.Task);
            }
            else
            {
                UpdateAtList(currentList, e.Task);
            }
        }

        private void UpdateAtList(ListType list, TaskItem task)
        {
            try
            {
                var aa = _typesToListDictionary[list];
                var itemInList = aa.First(x => x.ID == task.ID);
                itemInList.Name = task.Name;
                itemInList.DueToDate = task.DueToDate;
                itemInList.Completed = task.Completed;
            }
            catch (Exception)
            {
            }
        }

        private void MoveToProperList(ListType currentList, TaskItem task)
        {
            var item = _typesToListDictionary[currentList].First(x => x.ID == task.ID);
            _typesToListDictionary[currentList].Remove(item);
            PutEventToList(task);
        }

        private enum ListType
        {
            Undefined = 0,
            OldTasks = 1,
            CurrentTasks = 2,
            FollowingTasks
        }

        public void OnEvent(TaskItemDeleted e)
        {
            var list = LocalizeOnList(e.Task);
            var element = _typesToListDictionary[list].First(x => x.ID == e.Task.ID);
            _typesToListDictionary[list].Remove(element);
        }
    }
}

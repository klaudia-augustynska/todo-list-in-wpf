using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TodoList.DAL;
using TodoList.Helpers;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class MainWindowViewModel
    {
        public ICommand AddNewTaskCommand { get; private set; }
        public ICommand RefreshListsCommand { get; private set; }
        public ICommand ItemCheckedCommand { get; private set; }
        public ICommand ItemUncheckedCommand { get; private set; }
        public ICommand EditTodoCommand { get; private set; }
        public ICommand DeleteTodoCommand { get; private set; }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(async x => { await OnAddNewTaskCommandExecuted(); }, 
                CanAddNewTaskCommandExecute);
            RefreshListsCommand = new RelayCommand(async x => { await OnRefreshListCommandExecuted(); });
            ItemCheckedCommand = new RelayCommand(async x => { await OnItemCheckedCommand(x); });
            ItemUncheckedCommand = new RelayCommand(async x => { await OnItemUncheckedCommand(x); });
            EditTodoCommand = new RelayCommand(OnEditTodoCommand);
            DeleteTodoCommand = new RelayCommand(async x => { await OnDeleteTodoCommandExecute(x); });
        }

        private async Task OnDeleteTodoCommandExecute(object parameter)
        {
            var item = (TaskItem)parameter;
            _eventAggregator.Publish(new TaskItemDeleted(item));

            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    var itemToDelete = db.Tasks.Find(item.ID);
                    db.Tasks.Remove(itemToDelete);
                    db.SaveChanges();
                }
            });
        }

        private void OnEditTodoCommand(object parameter)
        {
            var item = (TaskItem) parameter;
            _eventAggregator.Publish(new EnableUserToEditTask(item));
        }

        private async Task OnItemUncheckedCommand(object parameter)
        {
            await ToggleItemComplete((TaskItem)parameter);
        }

        private async Task OnItemCheckedCommand(object parameter)
        {
            await ToggleItemComplete((TaskItem)parameter);
        }

        private async Task ToggleItemComplete(TaskItem item)
        {
            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    var itemInDb = db.Tasks.Find(item.ID);
                    itemInDb.Completed = item.Completed;
                    db.SaveChanges();
                }
            });
        }

        private async Task OnRefreshListCommandExecuted()
        {
            ObservableCollection<TaskItem> old = null, current = null, following = null;
            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    old = GetTasks(db, FindOldTasks);
                    current = GetTasks(db, FindCurrentTasks);
                    following = GetTasks(db, FindFollowingTasks);
                }
                Loading = false;
            });
            ReplaceList(OldTasks, old);
            ReplaceList(CurrentTasks, current);
            ReplaceList(FollowingTasks, following);
        }

        private void ReplaceList(ObservableCollection<TaskItem> listToReplace, ObservableCollection<TaskItem> replaceWith)
        {
            if (listToReplace == null || replaceWith == null)
                return;
            listToReplace.Clear();
            foreach (var item in replaceWith)
                listToReplace.Add(item);
        }

        private ObservableCollection<TaskItem> GetTasks(TodoListContext db, Func<TaskItem, bool> predicate)
        {
            try
            {
                var list = db.Tasks.Where(predicate).OrderBy(x => x.DueToDate).ToList();
                return new ObservableCollection<TaskItem>(list);
            }
            catch (ArgumentNullException)
            {
                return new ObservableCollection<TaskItem>();
            }
        }

        private bool CanAddNewTaskCommandExecute()
        {
            return !string.IsNullOrEmpty(TaskName);
        }

        private async Task OnAddNewTaskCommandExecuted()
        {
            var task = new TaskItem { Name = TaskName, DueToDate = SelectedDate};
            _eventAggregator.Publish(new TaskItemAdded(task));
            TaskName = string.Empty;
            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    db.Tasks.Add(task);
                    db.SaveChanges();
                }
            });
        }

        private bool FindOldTasks(TaskItem task)
        {
            return task.DueToDate.Date < SelectedDate.Date;
        }

        private bool FindCurrentTasks(TaskItem task)
        {
            return task.DueToDate.Date == SelectedDate.Date;
        }

        private bool FindFollowingTasks(TaskItem task)
        {
            return IsInThisWeek(task.DueToDate);
        }

        private bool IsInThisWeek(DateTime date)
        {
            int daysToLastDayOfWeek = 7 - (int)SelectedDate.DayOfWeek;
            DateTime endOfTheWeek = SelectedDate.AddDays(daysToLastDayOfWeek);
            if (date.Date > SelectedDate.Date && date.Date <= endOfTheWeek.Date)
                return true;
            return false;
        }
    }
}

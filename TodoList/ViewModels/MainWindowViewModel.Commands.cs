using System;
using System.Collections.Generic;
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

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(async () => { await OnAddNewTaskCommandExecuted(); }, 
                CanAddNewTaskCommandExecute);
        }

        private bool CanAddNewTaskCommandExecute()
        {
            return !string.IsNullOrEmpty(TaskName);
        }

        private async Task OnAddNewTaskCommandExecuted()
        {
            var task = new TaskItem { Name = TaskName };
            EventAggregator.Publish(new TaskItemAdded(task));
            TaskName = string.Empty;
            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    db.Tasks.Add(task);
                    db.SaveChangesAsync();
                }
            });
        }
    }
}

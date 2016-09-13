using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoList.DAL;
using TodoList.Helpers;
using TodoList.Helpers.EventAggregator;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class EditTaskWindowViewModel
    {
        public ICommand SaveItemCommand { get; private set; }
        public ICommand CancelEditingCommand { get; private set; }

        private void InitializeCommands()
        {
            SaveItemCommand = new RelayCommand(async x => { await OnSaveItemCommandExecute(); }, CanSaveItemCommandExecute);
            CancelEditingCommand = new RelayCommand(OnCancelEditingCommandExecute);
        }

        private void OnCancelEditingCommandExecute(object obj)
        {
            _eventAggregator.Publish(new TodoEditorShouldCloseItself());
        }

        private async Task OnSaveItemCommandExecute()
        {
            _eventAggregator.Publish(new TaskItemUpdated(Todo));
            _eventAggregator.Publish(new TodoEditorShouldCloseItself());
            await Task.Run(() =>
            {
                using (var db = new TodoListContext())
                {
                    var item = db.Tasks.Find(Todo.ID);
                    item.Name = Todo.Name;
                    item.Completed = Todo.Completed;
                    item.DueToDate = Todo.DueToDate;
                    db.SaveChanges();
                }
            });
        }

        private bool CanSaveItemCommandExecute()
        {
            if (string.IsNullOrWhiteSpace(Todo.Name)
                || Todo.ID == default(int)
                || Todo.ID == 0)
                return false;
            return true;
        }
    }
}

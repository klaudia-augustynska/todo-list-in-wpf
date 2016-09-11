using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.DesignViewModels
{
    public class MainWindowDesignViewModel
    {
        public List<TaskItem> OldTasks { get; set; }
        public List<TaskItem> CurrentTasks { get; set; }
        public List<TaskItem> FollowingTasks { get; set; }
        public string TaskName { get; set; }

        public MainWindowDesignViewModel()
        {
            //OldTasks = GetOldTasks();
            OldTasks = new List<TaskItem>();
            CurrentTasks = GetCurrentTasks();
            FollowingTasks = GetFollowingTasks();
            TaskName = string.Empty;
        }

        private List<TaskItem> GetFollowingTasks()
        {
            return new List<TaskItem>()
            {
                new TaskItem()
                {
                    Name = "Oddać książkę do biblioteki",
                    DueToDate = DateTime.Now.AddDays(1)
                },
                new TaskItem()
                {
                    Name = "Sprawdzić pociąg",
                    DueToDate = DateTime.Now.AddDays(1)
                }
            };
        }

        private List<TaskItem> GetCurrentTasks()
        {
            return new List<TaskItem>()
            {
                new TaskItem()
                {
                    Name = "Naprawić błędy w aplikacji",
                    DueToDate = DateTime.Now
                },
                new TaskItem()
                {
                    Name = "Iść biegać",
                    DueToDate = DateTime.Now
                }
            };
        }

        private List<TaskItem> GetOldTasks()
        {
            return new List<TaskItem>()
            {
                new TaskItem()
                {
                    Name = "Zjeść kanapkę",
                    DueToDate = new DateTime(2016,09,08)
                }
            };
        }
    }
}

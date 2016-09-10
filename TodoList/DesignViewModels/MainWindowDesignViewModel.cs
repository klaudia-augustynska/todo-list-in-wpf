using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.DesignViewModels
{
    public class MainWindowDesignViewModel
    {
        public List<Task> OldTasks { get; set; }
        public List<Task> CurrentTasks { get; set; }
        public List<Task> FollowingTasks { get; set; }

        public MainWindowDesignViewModel()
        {
            //OldTasks = GetOldTasks();
            OldTasks = new List<Task>();
            CurrentTasks = GetCurrentTasks();
            FollowingTasks = GetFollowingTasks();
        }

        private List<Task> GetFollowingTasks()
        {
            return new List<Task>()
            {
                new Task()
                {
                    Name = "Oddać książkę do biblioteki",
                    DueToDate = DateTime.Now.AddDays(1)
                },
                new Task()
                {
                    Name = "Sprawdzić pociąg",
                    DueToDate = DateTime.Now.AddDays(1)
                }
            };
        }

        private List<Task> GetCurrentTasks()
        {
            return new List<Task>()
            {
                new Task()
                {
                    Name = "Naprawić błędy w aplikacji",
                    DueToDate = DateTime.Now
                },
                new Task()
                {
                    Name = "Iść biegać",
                    DueToDate = DateTime.Now
                }
            };
        }

        private List<Task> GetOldTasks()
        {
            return new List<Task>()
            {
                new Task()
                {
                    Name = "Zjeść kanapkę",
                    DueToDate = new DateTime(2016,09,08)
                }
            };
        }
    }
}

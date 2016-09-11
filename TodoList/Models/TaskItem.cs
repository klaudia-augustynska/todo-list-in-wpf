using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TaskItem
    {
        private DateTime _dueToDate;

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated => DateTime.Now;

        public DateTime DueToDate
        {
            get
            {
                if (_dueToDate == default(DateTime))
                    return DateTime.Now;
                return _dueToDate;
            }
            set
            {
                _dueToDate = value;
            }
        }

        public bool Completed { get; set; }

        public TaskItem()
        {
            Completed = false;
        }
    }
}

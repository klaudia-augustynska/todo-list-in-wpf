using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TaskItem : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _dueToDate;
        private string _name;
        private bool _completed;

        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

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
                NotifyPropertyChanged(nameof(DueToDate));
            }
        }

        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                NotifyPropertyChanged(nameof(Completed));
            }
        }

        public TaskItem()
        {
            Completed = false;
        }

        public override string ToString()
        {
            return string.Format("{0}\t[{1}] {2} -- {3}", ID, Completed ? "+" : " ", Name, DueToDate);
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            TaskItem clone = new TaskItem
            {
                Name = _name,
                Completed = _completed,
                DueToDate = _dueToDate,
                ID = _id
            };
            return clone;
        }
    }
}

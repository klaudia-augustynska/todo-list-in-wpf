using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Helpers.EventAggregator
{
    public interface IEventAggregator
    {
        void Subscribe(object subscriber);
        void Publish<TEvent>(TEvent eventToPublish);
    }
}

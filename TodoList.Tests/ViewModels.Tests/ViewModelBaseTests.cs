using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TodoList.ViewModels;

namespace TodoList.Tests.ViewModels.Tests
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        [Test]
        public void ViewModelBase_SendsPropertyChanged()
        {
            var vm = new TestViewModel();
            var eventSent = false;
            vm.PropertyChanged += (s, e) => eventSent = true;

            vm.Property = "test";

            Assert.IsTrue(eventSent);
        }

        private class TestViewModel : ViewModelBase
        {
            private string _property;
            public string Property
            {
                get { return _property; }
                set
                {
                    _property = value;
                    NotifyPropertyChanged(nameof(Property));
                }
            }
        }
    }
}

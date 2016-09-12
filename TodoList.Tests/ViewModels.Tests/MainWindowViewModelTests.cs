using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using NUnit.Framework;
using TodoList.DAL;
using TodoList.Helpers.EventAggregator;
using TodoList.ViewModels;

namespace TodoList.Tests.ViewModels.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void MainWindowViewModel_InitsProperties()
        {
            var vm = new MainWindowViewModel();

            Assert.IsNotNull(vm.OldTasks);
            Assert.IsNotNull(vm.CurrentTasks);
            Assert.IsNotNull(vm.FollowingTasks);
            Assert.AreNotEqual(default(DateTime), vm.TaskDueToDate);
        }

        [Test]
        public void MainWindowViewModel_PropertiesRaiseCollectionChanged()
        {
            var propertiesChanged = new List<string>();
            var vm = new MainWindowViewModel();
            vm.OldTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((TaskItem)e.NewItems[0]).Name);
            vm.CurrentTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((TaskItem)e.NewItems[0]).Name);
            vm.FollowingTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((TaskItem)e.NewItems[0]).Name);

            vm.OldTasks.Add(new TaskItem() {Name = "test"});
            vm.CurrentTasks.Add(new TaskItem() { Name = "test" });
            vm.FollowingTasks.Add(new TaskItem() { Name = "test" });

            Assert.Contains("test", propertiesChanged);
            Assert.Contains("test", propertiesChanged);
            Assert.Contains("test", propertiesChanged);
            Assert.AreEqual("test", vm.OldTasks[0].Name);
            Assert.AreEqual("test", vm.CurrentTasks[0].Name);
            Assert.AreEqual("test", vm.FollowingTasks[0].Name);
        }

        [Test]
        public void AddNewTaskCommand_WhenEmptyTaskName_CannotExecute()
        {
            var vm = new MainWindowViewModel { TaskName = string.Empty };
            var canExecute = false;

            canExecute = vm.AddNewTaskCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Models;
using NUnit.Framework;
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
        }

        [Test]
        public void MainWindowViewModel_PropertiesRaiseCollectionChanged()
        {
            var propertiesChanged = new List<string>();
            var vm = new MainWindowViewModel();
            vm.OldTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((Task)e.NewItems[0]).Name);
            vm.CurrentTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((Task)e.NewItems[0]).Name);
            vm.FollowingTasks.CollectionChanged += (s, e) => propertiesChanged.Add(((Task)e.NewItems[0]).Name);

            vm.OldTasks.Add(new Task() {Name = "test"});
            vm.CurrentTasks.Add(new Task() { Name = "test" });
            vm.FollowingTasks.Add(new Task() { Name = "test" });

            Assert.Contains("test", propertiesChanged);
            Assert.Contains("test", propertiesChanged);
            Assert.Contains("test", propertiesChanged);
            Assert.AreEqual("test", vm.OldTasks[0].Name);
            Assert.AreEqual("test", vm.CurrentTasks[0].Name);
            Assert.AreEqual("test", vm.FollowingTasks[0].Name);
        }
    }
}

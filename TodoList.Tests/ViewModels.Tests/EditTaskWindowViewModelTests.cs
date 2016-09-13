using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.Tests.ViewModels.Tests
{
    [TestFixture]
    public class EditTaskWindowViewModelTests
    {
        [TestCase(0, "test")]
        [TestCase(default(int), "test")]
        [TestCase(39, "")]
        [TestCase(39, null)]
        [TestCase(39, "    ")]
        public void CanSaveItemCommandExecute_WhenFieldsNotFilled_ReturnsFalse(int id, string name)
        {
            var task = new TaskItem
            {
                ID = id,
                Name = name
            };
            var response = true;

            var vm = new EditTaskWindowViewModel(task);
            response = vm.SaveItemCommand.CanExecute(null);

            Assert.IsFalse(response);
        }

        [Test]
        public void CanSaveItemCommandExecute_WhenHaveIdAndName_ReturnsTrue()
        {
            var task = new TaskItem
            {
                ID = 39,
                Name = "test"
            };
            var response = false;

            var vm = new EditTaskWindowViewModel(task);
            response = vm.SaveItemCommand.CanExecute(null);

            Assert.IsTrue(response);
        }

    }
}

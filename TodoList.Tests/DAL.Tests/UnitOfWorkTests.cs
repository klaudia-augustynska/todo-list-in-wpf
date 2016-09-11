using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TodoList.DAL;
using TodoList.Models;

namespace TodoList.Tests.DAL.Tests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void UnitOfWork_CanConnect()
        {
            using (var db = new UnitOfWork())
            {
            }
        }

        [Test]
        public void UnitOfWork_CanCreate()
        {
            var exception = false;
            TaskItem task = null;
            const string taskName = "Testowe zadanie";
            var taskNameResult = string.Empty;

            using (var db = new UnitOfWork())
            {
                db.Tasks.Add(new TaskItem() { Name = taskName });
                db.Complete();

                try
                {
                    task = db.Tasks.GetAll().Last(x => true);
                }
                catch (ArgumentNullException)
                {
                    exception = true;
                }

                if (task != null)
                    taskNameResult = task.Name;

                db.Tasks.Remove(task);
                db.Complete();
            }

            Assert.IsFalse(exception);
            Assert.AreEqual(taskName, taskNameResult);
        }

        [Test]
        public void UnitOfWork_CanUpdate()
        {
            var task = new TaskItem() {Name = "name"};
            var taskNameAfter = "test";
            var taskNameResult = string.Empty;

            using (var db = new UnitOfWork())
            {
                db.Tasks.Add(task);
                db.Complete();
                task.Name = taskNameAfter;
                db.Complete();
                taskNameResult = db.Tasks.GetAll().Last(x => true).Name;
                db.Tasks.Remove(task);
                db.Complete();
            }

            Assert.AreEqual(taskNameAfter, taskNameResult);
        }

        [Test]
        public void UnitOfWork_CanDelete()
        {
            var task = new TaskItem() { Name = "name" };
            var idExists = false;

            using (var db = new UnitOfWork())
            {
                db.Tasks.Add(task);
                db.Complete();
                var taskId = task.ID;
                db.Tasks.Remove(task);
                db.Complete();
                if (db.Tasks.Get(taskId) != null)
                    idExists = true;
            }

            Assert.IsFalse(idExists);
        }

    }
}

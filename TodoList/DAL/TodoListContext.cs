using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using TodoList.Models;

namespace TodoList.DAL
{
    public class TodoListContext : DbContext
    {
        public TodoListContext() : base("TodoListContext")
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

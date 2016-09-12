using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DAL.Repositories;

namespace TodoList.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoListContext _context;

        public UnitOfWork() : this(new TodoListContext())
        {
        }

        public UnitOfWork(TodoListContext context)
        {
            _context = context;
            Tasks = new TaskItemRepository(_context);
        }

        public ITaskItemRepository Tasks { get; private set; }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

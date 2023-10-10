using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using TodoList.Api.Service;
using TodoList.Api.RepoInterfaces;
using System.Linq;

namespace TodoList.Api.Repositories
{
    public class ToDoItemRepository : ITodoItemsRepoInterface
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoItemService> _logger;
        public ToDoItemRepository(TodoContext context, ILogger<TodoItemService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<TodoItem>> GetTodoItems()
        {
            List<TodoItem> result = new List<TodoItem>();
            try
            {
                result = await _context.TodoItems.Where(x => !x.IsCompleted).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }

        public async ValueTask<TodoItem> GetTodoItem(Guid id)
        {
            TodoItem result = null;
            try
            {
                result = await _context.TodoItems.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }


        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return null;
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }
        private bool TodoItemIdExists(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }

        private bool TodoItemDescriptionExists(string description)
        {
            return _context.TodoItems
                   .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
        }
    }
}


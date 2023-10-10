using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TodoList.Api.ServiceInterfaces;
using TodoList.Api.RepoInterfaces;
using Microsoft.Extensions.Logging;

namespace TodoList.Api.Service
{
    public class TodoItemService : ITodoItemsServiceInterface
    {
        private readonly ILogger<TodoItemService> _logger;
        private readonly ITodoItemsRepoInterface _todoItemsRepoInterface;
        public TodoItemService(ITodoItemsRepoInterface todoItemsRepoInterface, ILogger<TodoItemService> logger)
        {
            _todoItemsRepoInterface = todoItemsRepoInterface;
            _logger = logger;
        }
        public async Task<List<TodoItem>> GetTodoItems()
        {
            List<TodoItem> result = new List<TodoItem>();
            try
            {
                result = await _todoItemsRepoInterface.GetTodoItems();

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
                result = await _todoItemsRepoInterface.GetTodoItem(id);
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
            try
            {
                await _todoItemsRepoInterface.PutTodoItem(id, todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return null;
        }

    }
}

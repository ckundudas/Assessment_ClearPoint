using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Api.ServiceInterfaces
{
    public interface ITodoItemsServiceInterface
    {
        Task<List<TodoItem>> GetTodoItems();
        ValueTask<TodoItem> GetTodoItem(Guid id);
        Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem);
    }
}

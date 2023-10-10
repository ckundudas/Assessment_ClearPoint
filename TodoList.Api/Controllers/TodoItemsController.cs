using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.ServiceInterfaces;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemsServiceInterface _todoItemsInterface;
        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItemsServiceInterface todoItemsInterface)
        {
            _todoItemsInterface = todoItemsInterface;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet("get-todo-items")]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = await _todoItemsInterface.GetTodoItems();
            return Ok(results);
        }

        // GET: api/TodoItems/...
        [HttpGet("get-todo-item/{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var result = await _todoItemsInterface.GetTodoItem(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("put-todo-item")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _todoItemsInterface.PutTodoItem(id, todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return NoContent();
        }
    }
}

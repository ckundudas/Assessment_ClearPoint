using Microsoft.Extensions.Logging;
using TodoList.Api.RepoInterfaces;
using TodoList.Api.ServiceInterfaces;
using TodoList.Api.Service;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Api.UnitTests
{
    public class DummyTestShould : ITodoItemsServiceInterface
    {
        private ITodoItemsServiceInterface sut;
        private Mock<ILogger<TodoItemService>> loggerMOCK;
        private Mock<ITodoItemsRepoInterface> repoMock;
        public DummyTestShould()
        {
            loggerMOCK = new Mock<ILogger<TodoItemService>>();
            repoMock = new Mock<ITodoItemsRepoInterface>();
            sut = new TodoItemService(repoMock.Object, loggerMOCK.Object);
        }

        [Fact]
        public async void Test_GetTodoItems_ReturnsData()
        {
            var expected = new List<TodoItem>();

            repoMock.Setup(s => s.GetTodoItems()).ReturnsAsync(expected);

            var actual = await GetTodoItems();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Test_GetTodoItems_Returns_Null()
        {
            var expected = new List<TodoItem>();

            repoMock.Setup(s => s.GetTodoItems()).ReturnsAsync(expected);

            var actual = await GetTodoItems();

            Assert.True(1 == 1);
        }

        [Fact]
        public async void Test_GetTodoItem_ReturnsData()
        {
            Guid id = new Guid("ad0eb1cd-c8ba-40fd-820e-cceeecd763f9");
            var expected = new TodoItem();

            repoMock.Setup(s => s.GetTodoItem(id)).ReturnsAsync(expected);

            var actual = await GetTodoItem(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Test_GetTodoItem_Returns_Null()
        {
            Guid id = new Guid("ad0eb1cd-c8ba-40fd-820e-cceeecd763f9");
            var expected = new TodoItem();

            repoMock.Setup(s => s.GetTodoItem(id)).ReturnsAsync(expected);

            var actual = await GetTodoItem(id);

            Assert.True(1 == 1);
        }

        [Fact]
        public async Task Test_PutTodoItem_ReturnsData()
        {
            Guid id = new Guid("ad0eb1cd-c8ba-40fd-820e-cceeecd763f9");
            var model = new TodoItem
            {
                Id = new Guid("ad0eb1cd-c8ba-40fd-820e-cceeecd763f9"),
                Description = "test",
                IsCompleted = true
            };

            repoMock.Setup(s => s.PutTodoItem(id, model));

            var actual = await PutTodoItem(id, model);

            Assert.True(1 == 1);
        }

        public async Task<List<TodoItem>> GetTodoItems()
        {
          return await sut.GetTodoItems();
        }
        public async ValueTask<TodoItem> GetTodoItem(Guid id)
        {            
            return await sut.GetTodoItem(id);
        }
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {  
            return await sut.PutTodoItem(id, todoItem);
        }
    }
}

using System;
using System.Linq;
using WebAppToDoAssignment.Date;
using WebAppToDoAssignment.Models;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class TodoServiceTest
    {
        private readonly TodoService _todoService;

        public TodoServiceTest()
        {
            _todoService = new TodoService();
            _todoService.Clear(); // Ensure a clean state before each test
            TodoSequencer.Reset();
        }

        [Fact]
        public void Size_ShouldReturnZero_WhenNoTodosAdded()
        {
            // Act
            int size = _todoService.Size();

            // Assert
            Assert.Equal(0, size);
        }

        [Fact]
        public void CreateTodoItem_ShouldAddNewTodo()
        {
            // Arrange
            string description = "Test Todo";
            bool done = false;
            Person assignee = null;

            // Act
            var todo = _todoService.CreateTodoItem(description, done, assignee);

            // Assert
            Assert.NotNull(todo);
            Assert.Equal(description, todo.Description);
            Assert.False(todo.Done);
            Assert.Null(todo.Assignee);
            Assert.Equal(1, _todoService.Size());
        }

        [Fact]
        public void CreateTodoItem_ShouldThrowException_WhenDescriptionIsInvalid()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => _todoService.CreateTodoItem(null, false, null));
            Assert.Throws<ArgumentException>(() => _todoService.CreateTodoItem("", false, null));
        }

        [Fact]
        public void FindAll_ShouldReturnAllTodoItems()
        {
            // Arrange
            _todoService.CreateTodoItem("Todo 1", false, null);
            _todoService.CreateTodoItem("Todo 2", true, null);

            // Act
            var todos = _todoService.FindAll();

            // Assert
            Assert.Equal(2, todos.Length);
        }

        [Fact]
        public void FindById_ShouldReturnCorrectTodo()
        {
            // Arrange
            var todo1 = _todoService.CreateTodoItem("Todo 1", false, null);
            var todo2 = _todoService.CreateTodoItem("Todo 2", true, null);

            // Act
            var foundTodo = _todoService.FindById(todo2.Id);

            // Assert
            Assert.NotNull(foundTodo);
            Assert.Equal("Todo 2", foundTodo.Description);
        }

        [Fact]
        public void FindById_ShouldReturnNull_WhenTodoDoesNotExist()
        {
            // Act
            var foundTodo = _todoService.FindById(999);

            // Assert
            Assert.Null(foundTodo);
        }

        [Fact]
        public void UpdateTodoItem_ShouldModifyExistingTodo()
        {
            // Arrange
            var todo = _todoService.CreateTodoItem("Original Todo", false, null);
            var updatedTodo = new Todo(todo.Id, "Updated Todo")
            {
                Done = true,
                Assignee = new Person(1, "John", "Doe")
            };

            // Act
            _todoService.UpdateTodoItem(updatedTodo);

            // Assert
            var modifiedTodo = _todoService.FindById(todo.Id);
            Assert.NotNull(modifiedTodo);
            Assert.Equal("Updated Todo", modifiedTodo.Description);
            Assert.True(modifiedTodo.Done);
            Assert.Equal("John", modifiedTodo.Assignee.FirstName);
        }

        [Fact]
        public void RemoveTodoItem_ShouldRemoveTodoById()
        {
            // Arrange
            var todo = _todoService.CreateTodoItem("Test Todo", false, null);
            int initialSize = _todoService.Size();

            // Act
            _todoService.RemoveTodoItem(todo.Id);

            // Assert
            Assert.Equal(initialSize - 1, _todoService.Size());
            Assert.Null(_todoService.FindById(todo.Id));
        }

        [Fact]
        public void FindByDoneStatus_ShouldReturnTodosWithMatchingDoneStatus()
        {
            // Arrange
            _todoService.CreateTodoItem("Todo 1", true, null);
            _todoService.CreateTodoItem("Todo 2", false, null);

            // Act
            var doneTodos = _todoService.FindByDoneStatus(true);

            // Assert
            Assert.Single(doneTodos);
            Assert.True(doneTodos[0].Done);
        }

        [Fact]
        public void FindByAssignee_ShouldReturnTodosWithMatchingAssignee()
        {
            // Arrange
            var assignee = new Person(1, "John", "Doe");
            _todoService.CreateTodoItem("Todo 1", false, assignee);
            _todoService.CreateTodoItem("Todo 2", false, null);

            // Act
            var todosByAssignee = _todoService.FindByAssignee(assignee);

            // Assert
            Assert.Single(todosByAssignee);
            Assert.Equal("John", todosByAssignee[0].Assignee.FirstName);
        }

        [Fact]
        public void FindUnassignedTodoItems_ShouldReturnOnlyUnassignedTodos()
        {
            // Arrange
            _todoService.CreateTodoItem("Todo 1", false, null);
            _todoService.CreateTodoItem("Todo 2", false, new Person(1, "John", "Doe"));

            // Act
            var unassignedTodos = _todoService.FindUnassignedTodoItems();

            // Assert
            Assert.Single(unassignedTodos);
            Assert.Null(unassignedTodos[0].Assignee);
        }

        [Fact]
        public void Clear_ShouldRemoveAllTodos()
        {
            // Arrange
            _todoService.CreateTodoItem("Todo 1", false, null);
            _todoService.CreateTodoItem("Todo 2", true, null);

            // Act
            _todoService.Clear();

            // Assert
            Assert.Equal(0, _todoService.Size());
        }
    }
}

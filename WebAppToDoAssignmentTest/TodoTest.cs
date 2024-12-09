using System;
using WebAppToDoAssignment.Models;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class TodoTest
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeProperties()
        {
            // Arrange
            int id = 1;
            string description = "Complete the assignment";

            // Act
            var todo = new Todo(id, description);

            // Assert
            Assert.Equal(id, todo.Id);
            Assert.Equal(description, todo.Description);
            Assert.False(todo.Done); // Default value
            Assert.Null(todo.Assignee); // Default value
        }

        [Fact]
        public void Constructor_Parameterless_ShouldInitializeWithDefaults()
        {
            // Act
            var todo = new Todo();

            // Assert
            Assert.Equal(0, todo.Id); // Default value for int
            Assert.Null(todo.Description); // Default value for string
            Assert.False(todo.Done); // Default value for bool
            Assert.Null(todo.Assignee); // Default value for Person
        }

        [Fact]
        public void Description_SetValidValue_ShouldUpdateDescription()
        {
            // Arrange
            var todo = new Todo();
            string validDescription = "Write unit tests";

            // Act
            todo.Description = validDescription;

            // Assert
            Assert.Equal(validDescription, todo.Description);
        }

        [Fact]
        public void Description_SetNullOrEmptyValue_ShouldThrowArgumentException()
        {
            // Arrange
            var todo = new Todo();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => todo.Description = null);
            Assert.Throws<ArgumentException>(() => todo.Description = "");
        }

        [Fact]
        public void Done_SetTrue_ShouldUpdateDoneStatus()
        {
            // Arrange
            var todo = new Todo();

            // Act
            todo.Done = true;

            // Assert
            Assert.True(todo.Done);
        }

        [Fact]
        public void Assignee_SetValidPerson_ShouldUpdateAssignee()
        {
            // Arrange
            var todo = new Todo();
            var person = new Person(1, "John", "Doe");

            // Act
            todo.Assignee = person;

            // Assert
            Assert.Equal(person, todo.Assignee);
        }

        [Fact]
        public void Id_ShouldBeReadOnly()
        {
            // Arrange
            int id = 5;
            var todo = new Todo(id, "Sample description");

            // Act
            var result = todo.Id;

            // Assert
            Assert.Equal(id, result);
        }
    }
}

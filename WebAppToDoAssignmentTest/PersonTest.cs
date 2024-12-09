using System;
using WebAppToDoAssignment.Models;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class PersonTest
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeProperties()
        {
            // Arrange
            int id = 1;
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var person = new Person(id, firstName, lastName);

            // Assert
            Assert.Equal(id, person.Id);
            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
        }

        [Fact]
        public void Constructor_Parameterless_ShouldInitializeWithDefaults()
        {
            // Act
            var person = new Person();

            // Assert
            Assert.Equal(0, person.Id); // Default value for int
            Assert.Null(person.FirstName);
            Assert.Null(person.LastName);
        }

        [Fact]
        public void FirstName_SetValidValue_ShouldUpdateFirstName()
        {
            // Arrange
            var person = new Person();
            string validFirstName = "Alice";

            // Act
            person.FirstName = validFirstName;

            // Assert
            Assert.Equal(validFirstName, person.FirstName);
        }

        [Fact]
        public void FirstName_SetNullOrEmptyValue_ShouldThrowArgumentException()
        {
            // Arrange
            var person = new Person();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => person.FirstName = null);
            Assert.Throws<ArgumentException>(() => person.FirstName = "");
        }

        [Fact]
        public void LastName_SetValidValue_ShouldUpdateLastName()
        {
            // Arrange
            var person = new Person();
            string validLastName = "Smith";

            // Act
            person.LastName = validLastName;

            // Assert
            Assert.Equal(validLastName, person.LastName);
        }

        [Fact]
        public void LastName_SetNullOrEmptyValue_ShouldThrowArgumentException()
        {
            // Arrange
            var person = new Person();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => person.LastName = null);
            Assert.Throws<ArgumentException>(() => person.LastName = "");
        }

        [Fact]
        public void Id_ShouldBeReadOnly()
        {
            // Arrange
            int id = 10;
            var person = new Person(id, "Jane", "Doe");

            // Act
            var result = person.Id;

            // Assert
            Assert.Equal(id, result);
        }
    }
}

using System;
using System.Linq;
using WebAppToDoAssignment.Date;
using WebAppToDoAssignment.Models;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class PeopleServiceTest
    {
        private readonly PeopleService _peopleService;

        public PeopleServiceTest()
        {
            _peopleService = new PeopleService();
            _peopleService.Clear(); // Ensure a clean state before each test
        }

        [Fact]
        public void Size_ShouldReturnZero_WhenNoPeopleAdded()
        {
            // Act
            int size = _peopleService.Size();

            // Assert
            Assert.Equal(0, size);
        }

        [Fact]
        public void CreatePerson_ShouldAddNewPerson()
        {
            // Act
            var person = _peopleService.CreatePerson("John", "Doe");

            // Assert
            Assert.NotNull(person);
            Assert.Equal(1, _peopleService.Size());
            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
        }

        [Fact]
        public void FindAll_ShouldReturnAllPeople()
        {
            // Arrange
            _peopleService.CreatePerson("John", "Doe");
            _peopleService.CreatePerson("Jane", "Smith");

            // Act
            var allPeople = _peopleService.FindAll();

            // Assert
            Assert.Equal(2, allPeople.Length);
        }

        [Fact]
        public void FindById_ShouldReturnCorrectPerson()
        {
            // Arrange
            var person1 = _peopleService.CreatePerson("John", "Doe");
            var person2 = _peopleService.CreatePerson("Jane", "Smith");

            // Act
            var foundPerson = _peopleService.FindById(person2.Id);

            // Assert
            Assert.NotNull(foundPerson);
            Assert.Equal("Jane", foundPerson.FirstName);
            Assert.Equal("Smith", foundPerson.LastName);
        }

        [Fact]
        public void FindById_ShouldReturnNull_WhenPersonNotFound()
        {
            // Act
            var foundPerson = _peopleService.FindById(999); // Non-existent ID

            // Assert
            Assert.Null(foundPerson);
        }

        [Fact]
        public void RemovePerson_ShouldRemovePersonById()
        {
            // Arrange
            var person = _peopleService.CreatePerson("John", "Doe");
            int initialSize = _peopleService.Size();

            // Act
            _peopleService.RemovePerson(person.Id);

            // Assert
            Assert.Equal(initialSize - 1, _peopleService.Size());
            Assert.Null(_peopleService.FindById(person.Id));
        }

        [Fact]
        public void RemovePerson_ShouldDoNothing_WhenPersonIdDoesNotExist()
        {
            // Arrange
            _peopleService.CreatePerson("John", "Doe");
            int initialSize = _peopleService.Size();

            // Act
            _peopleService.RemovePerson(999); // Non-existent ID

            // Assert
            Assert.Equal(initialSize, _peopleService.Size());
        }

        [Fact]
        public void Clear_ShouldRemoveAllPeople()
        {
            // Arrange
            _peopleService.CreatePerson("John", "Doe");
            _peopleService.CreatePerson("Jane", "Smith");

            // Act
            _peopleService.Clear();

            // Assert
            Assert.Equal(0, _peopleService.Size());
        }
    }
}


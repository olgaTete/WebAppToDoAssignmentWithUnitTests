using WebAppToDoAssignment.Date;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class TodoSequencerTest
    {
        [Fact]
        public void NextTodoId_ShouldIncrementTodoId()
        {
            // Arrange
            TodoSequencer.Reset(); // Ensure the initial state is consistent

            // Act
            int firstId = TodoSequencer.NextTodoId();
            int secondId = TodoSequencer.NextTodoId();

            // Assert
            Assert.Equal(1, firstId);
            Assert.Equal(2, secondId);
        }

        [Fact]
        public void Reset_ShouldResetTodoIdToZero()
        {
            // Arrange
            TodoSequencer.NextTodoId(); // Increment at least once
            TodoSequencer.NextTodoId(); // Increment again

            // Act
            TodoSequencer.Reset();
            int idAfterReset = TodoSequencer.NextTodoId();

            // Assert
            Assert.Equal(1, idAfterReset); // After reset, the first ID should be 1
        }

        [Fact]
        public void MultipleCallsToNextTodoId_ShouldReturnSequentialIds()
        {
            // Arrange
            TodoSequencer.Reset();

            // Act
            int id1 = TodoSequencer.NextTodoId();
            int id2 = TodoSequencer.NextTodoId();
            int id3 = TodoSequencer.NextTodoId();

            // Assert
            Assert.Equal(1, id1);
            Assert.Equal(2, id2);
            Assert.Equal(3, id3);
        }
    }
}

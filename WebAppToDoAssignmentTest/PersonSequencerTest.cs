using WebAppToDoAssignment.Date;
using Xunit;

namespace WebAppToDoAssignmentTest
{
    public class PersonSequencerTest
    {
        [Fact]
        public void NextPersonId_ShouldIncrementPersonId()
        {
            // Arrange
            PersonSequencer.Reset(); // Ensure the initial state is consistent

            // Act
            int firstId = PersonSequencer.NextPersonId();
            int secondId = PersonSequencer.NextPersonId();

            // Assert
            Assert.Equal(1, firstId);
            Assert.Equal(2, secondId);
        }

        [Fact]
        public void Reset_ShouldResetPersonIdToZero()
        {
            // Arrange
            PersonSequencer.NextPersonId(); // Increment at least once
            PersonSequencer.NextPersonId(); // Increment again

            // Act
            PersonSequencer.Reset();
            int idAfterReset = PersonSequencer.NextPersonId();

            // Assert
            Assert.Equal(1, idAfterReset); // After reset, the first ID should be 1
        }

        [Fact]
        public void MultipleCallsToNextPersonId_ShouldReturnSequentialIds()
        {
            // Arrange
            PersonSequencer.Reset();

            // Act
            int id1 = PersonSequencer.NextPersonId();
            int id2 = PersonSequencer.NextPersonId();
            int id3 = PersonSequencer.NextPersonId();

            // Assert
            Assert.Equal(1, id1);
            Assert.Equal(2, id2);
            Assert.Equal(3, id3);
        }
    }
}

using TipTracker.Core;
using Xunit;

namespace Core.Tests
{

    public class EventShould
    {
        [Fact]
        public void CorrectlyDetermineEquivalence()
        {
            var testEvent = new Event { Name = "Test Event 1" };
            var testSame = new Event { Name = testEvent.Name };
            var testDifferent = new Event { Name = "Test Event 2" };

            var bothNull = Event.AreEquivalent(null, null);
            var firstNull = Event.AreEquivalent(null, testEvent);
            var secondNull = Event.AreEquivalent(testEvent, null);
            var sameInstance = Event.AreEquivalent(testEvent, testEvent);
            var differentInstancesSameEvent = Event.AreEquivalent(testEvent, testSame);
            var differentEvents = Event.AreEquivalent(testEvent, testDifferent);

            Assert.True(bothNull);
            Assert.False(firstNull);
            Assert.False(secondNull);
            Assert.True(sameInstance);
            Assert.True(differentInstancesSameEvent);
            Assert.False(differentEvents);
        }
    }
}

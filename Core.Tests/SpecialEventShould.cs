using TipTracker.Core;
using Xunit;

namespace Core.Tests
{

    public class SpecialEventShould
    {
        [Fact]
        public void CorrectlyDetermineEquivalence()
        {
            var testEvent = new SpecialEvent { Name = "Test Event 1" };
            var testSame = new SpecialEvent { Name = testEvent.Name };
            var testDifferent = new SpecialEvent { Name = "Test Event 2" };

            var bothNull = SpecialEvent.AreEquivalent(null, null);
            var firstNull = SpecialEvent.AreEquivalent(null, testEvent);
            var secondNull = SpecialEvent.AreEquivalent(testEvent, null);
            var sameInstance = SpecialEvent.AreEquivalent(testEvent, testEvent);
            var differentInstancesSameEvent = SpecialEvent.AreEquivalent(testEvent, testSame);
            var differentEvents = SpecialEvent.AreEquivalent(testEvent, testDifferent);

            Assert.True(bothNull);
            Assert.False(firstNull);
            Assert.False(secondNull);
            Assert.True(sameInstance);
            Assert.True(differentInstancesSameEvent);
            Assert.False(differentEvents);
        }
    }
}
using TipTracker.Core;
using Xunit;

namespace Core.Tests
{
    public class ServerShould
    {
        [Fact]
        public void CreateCloneWithSameValues()
        {
            var expected = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true

            };

            var result = expected.Clone();

            Assert.Equal(expected.PosId, result.PosId);
            Assert.Equal(expected.FirstName, result.FirstName);
            Assert.Equal(expected.LastName, result.LastName);
            Assert.Equal(expected.SuppressChit, result.SuppressChit);
        }

        [Fact]
        public void CreateDistinctCloneInstance()
        {
            var expected = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };

            var result = expected.Clone();
            
            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void UsePosIdForUniqueness()
        {
            var instanceId = "1000";

            var first = new Server
            {
                PosId = instanceId
            };
            var second = new Server
            {
                PosId = instanceId
            };

            var result = first.Is(second);

            Assert.True(result);
        }

        [Fact]
        public void ConsiderTwoInstancesWithAllSamePropertiesMatching()
        {
            var first = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };
            var second = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };

            var result = first.Matches(second);

            Assert.True(result);
        }

        [Fact]
        public void ConsiderTwoInstancesDifferentPosIdsNonMatching()
        {
            var first = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };
            var second = new Server
            {
                PosId = "1001",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };

            var result = first.Matches(second);

            Assert.False(result);
        }

        [Fact]
        public void ConsiderNullOtherInstanceNonMatching()
        {
            var server = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };

            var result = server.Matches(null);

            Assert.False(result);
        }

        [Fact]
        public void ConsiderOtherEquivalentInstanceWithDifferentPropertiesNonMatching()
        {
            var testServer = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = true
            };
            var differentFirstName = new Server
            {
                PosId = "1000",
                FirstName = "John",
                LastName = "Server",
                SuppressChit = true
            };
            var differentLastName = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Waiter",
                SuppressChit = true
            };
            var differentChitSuppression = new Server
            {
                PosId = "1000",
                FirstName = "George",
                LastName = "Server",
                SuppressChit = false
            };

            var firstNameMatches = testServer.Matches(differentFirstName);
            var lastNameMatches = testServer.Matches(differentLastName);
            var chitSuppressionMatches = testServer.Matches(differentChitSuppression);

            Assert.False(firstNameMatches);
            Assert.False(lastNameMatches);
            Assert.False(chitSuppressionMatches);
        }
    }
}
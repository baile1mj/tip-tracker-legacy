using System;
using System.Collections.Generic;
using TipTracker.Core;
using Xunit;

namespace Core.Tests
{
    public class PayPeriodShould
    {
        [Fact]
        public void PreventCreatingInstanceWithStartDateAfterEndDate()
        {
            var start = DateTime.Today;
            var end = start.AddDays(-1);

            Assert.Throws<InvalidOperationException>(() => new PayPeriod(start, end, null));
        }

        [Fact]
        public void CreateInstanceWithCorrectBusinessDateWhenSpecified()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var businessDate = start.AddDays(2);
            var testClass = new PayPeriod(start, end, businessDate, new List<Server>());

            Assert.Equal(businessDate, testClass.BusinessDate);
        }

        [Fact]
        public void CreateInstanceWithPeriodStartAsBusinessDateWhenNotSpecified()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var testClass = new PayPeriod(start, end, new List<Server>());

            Assert.Equal(start, testClass.BusinessDate);
        }

        [Fact]
        public void CreateInstanceWithEmptyServerListWhenGivenNullList()
        {
            var testClass = new PayPeriod(DateTime.Today, DateTime.Today, null);

            Assert.Empty(testClass.Servers);
        }

        [Fact]
        public void CreateInstanceWithExpectedServerListWhenGivenServers()
        {
            var servers = new List<Server>
            {
                new Server { PosId = "1000", FirstName = "John", LastName = "Denver" },
                new Server { PosId = "1001", FirstName = "George", LastName = "Jones" }
            };
            var testClass = new PayPeriod(DateTime.Today, DateTime.Today, servers);

            Assert.Collection(testClass.Servers,
                s => Assert.Same(servers[0], s),
                s => Assert.Same(servers[1], s)
            );
        }

        [Fact]
        public void PreventCreatingInstanceWithBusinessDateBeforePeriodStart()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var businessDate = start.AddDays(-1);

            Assert.Throws<InvalidOperationException>(() => new PayPeriod(start, end, businessDate, new List<Server>()));
        }

        [Fact]
        public void PreventCreatingInstanceWithBusinessDateAfterPeriodEnd()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var businessDate = end.AddDays(1);

            Assert.Throws<InvalidOperationException>(() => new PayPeriod(start, end, businessDate, new List<Server>()));
        }

        [Fact]
        public void PreventSettingBusinessDateBeforePeriodStart()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var testClass = new PayPeriod(start, end, new List<Server>());

            Assert.Throws<InvalidOperationException>(() => testClass.BusinessDate = start.AddDays(-1));
        }

        [Fact]
        public void PreventSettingBusinessDateAfterPeriodEnd()
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            var testClass = new PayPeriod(start, end, new List<Server>());

            Assert.Throws<InvalidOperationException>(() => testClass.BusinessDate = end.AddDays(1));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker.Core.Reporting
{
    /// <summary>
    /// Provides methods for preparing the data to print on a tip chit.
    /// </summary>
    public class TipChitDataBuilder 
    {
        private readonly DateTime _payPeriodStart;
        private readonly DateTime _payPeriodEnd;
        private readonly List<TipType> _tipTypes;

        /// <summary>
        /// The server for whom the chit is prepared.
        /// </summary>
        public Server Server { get; }

        /// <summary>
        /// Gets the collection of tips that are summarized by day and type.
        /// </summary>
        public List<Tip> DailySummaryTips { get; private set; } = new List<Tip>();

        /// <summary>
        /// Gets the collection of tips that are detailed by event.
        /// </summary>
        public List<Tip> EventTips { get; private set; } = new List<Tip>();

        /// <summary>
        /// Gets the collection of tips that are claimed by or on behalf of the server.
        /// </summary>
        public List<Tip> ClaimedTips { get; private set; } = new List<Tip>();

        /// <summary>
        /// Creates a new instance of the data class.
        /// </summary>
        /// <param name="payPeriod">The pay period for which the chit applies.</param>
        /// <param name="server">The server who will receive the chit.</param>
        /// <param name="tipTypes">The collection of types of tips the server may have received.</param>
        public TipChitDataBuilder(PayPeriod payPeriod, Server server, IEnumerable<TipType> tipTypes)
        {
            if (payPeriod == null)
            {
                throw new ArgumentNullException(nameof(payPeriod), "Cannot create tip chit when no pay period is specified.");
            }

            _payPeriodStart = payPeriod.Start;
            _payPeriodEnd = payPeriod.End;

            if (server == null)
            {
                throw new ArgumentNullException(nameof(server), "Cannot create tip chit when no server is specified.");
            }

            Server = server;
            _tipTypes = tipTypes?.ToList();

            if (_tipTypes == null || _tipTypes.Count == 0)
            {
                throw new ArgumentException("Tip types must be specified so that tips may be categorized.");
            }

            CategorizeTips();
            PrepareSummaryTips();
            PrepareEventTips();
            PrepareClaimedTips();
        }

        /// <summary>
        /// Gets the collection of tips to print on the report.
        /// </summary>
        /// <returns>The server's tips to include on the chit.</returns>
        public IEnumerable<Tip> GetPreparedTips()
        {
            return DailySummaryTips.Union(EventTips).Union(ClaimedTips);
        }
        
        /// <summary>
        /// Categorizes the tips into their appropriate reporting types.
        /// </summary>
        private void CategorizeTips()
        {
            // TODO: Need a cleaner way to handle the business logic since it's duplicated in multiple places.
            foreach (var tip in Server.Tips)
            {
                if (tip.Type.IsClaimedTip)
                {
                    ClaimedTips.Add(tip);
                }
                else if (tip.Type.IsEventOriginated)
                {
                    EventTips.Add(tip);
                }
                else
                {
                    DailySummaryTips.Add(tip);
                }
            }
        }

        /// <summary>
        /// Prepares the tips that will be summarized by day and type.
        /// </summary>
        private void PrepareSummaryTips()
        {
            var summaryTipTypes = _tipTypes.Where(t => !t.IsClaimedTip && !t.IsEventOriginated);
            var dailyPayableTipTotals = new Dictionary<DateTime, Dictionary<TipType, decimal>>();
            var currentDate = _payPeriodStart;

            // We need a place to store totals for the cartesian product of days and types.
            while (currentDate <= _payPeriodEnd)
            {
                dailyPayableTipTotals.Add(currentDate, summaryTipTypes.ToDictionary(key => key, value => 0m));
                currentDate = currentDate.AddDays(1);
            }

            // Calculate the total for each combination.
            foreach (var tip in DailySummaryTips)
            {
                dailyPayableTipTotals[tip.EarnedOn][tip.Type] += tip.Amount;
            }

            // Return a collection of the aggregated totals.
            DailySummaryTips = dailyPayableTipTotals
                .SelectMany(typeTotalsByDate => typeTotalsByDate.Value
                    .Select(totalByType => new Tip
                    {
                        Amount = totalByType.Value,
                        EarnedBy = Server,
                        EarnedOn = typeTotalsByDate.Key,
                        Type = totalByType.Key
                    })
                )
                .ToList();
        }

        /// <summary>
        /// Prepares the tips that will list event details.
        /// </summary>
        private void PrepareEventTips()
        {
            EventTips = EventTips
                .GroupBy(t => t.Event)
                .Select(g => new Tip
                {
                    Amount = g.Sum(t => t.Amount),
                    EarnedBy = g.First().EarnedBy,
                    EarnedOn = g.Key.Date,
                    Event = g.Key,
                    Type = g.First().Type
                })
                .OrderBy(t => t.EarnedOn)
                .ToList();
        }

        /// <summary>
        /// Prepares the claimed tips.
        /// </summary>
        private void PrepareClaimedTips()
        {
            ClaimedTips = ClaimedTips
                .GroupBy(t => t.Type)
                .Select(g => new Tip
                {
                    Amount = g.Sum(t => t.Amount),
                    EarnedBy = g.First().EarnedBy,
                    EarnedOn = g.First().EarnedOn,
                    Type = g.Key
                })
                .OrderBy(t => t.EarnedOn)
                .ToList();
        }
    }
}
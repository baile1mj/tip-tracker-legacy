using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker.Core.Reporting
{
    public class TipTypeTotal
    {
        public TipType Type { get; }

        public TipClassification Classification { get; }

        public bool IsClaimed { get; }

        public int Count { get; }

        public decimal Total { get; }

        public TipTypeTotal(TipType type, int count, decimal total)
        {
            Type = type;
            Classification = type.Classification;
            IsClaimed = type.IsClaimedTip;
            Count = count;
            Total = total;
        }

        public object ToAnonymous()
        {
            return new
            {
                Type = Type.Name,
                Classification = Classification.Description,
                IsClaimed = IsClaimed,
                Count = Count,
                Total = Total
            };
        }
    }
}
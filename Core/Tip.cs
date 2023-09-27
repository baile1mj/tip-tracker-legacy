using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker
{
    public class Tip
    {
        public int Number { get; }
        public TipType Type { get; }
        public DateOnly EarnedOn { get; }
        public Server EarnedBy { get; }
        public decimal Amount { get; }
    }
}

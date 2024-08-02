using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{



    public sealed class PayFrequency
    {
        public string Description { get; }

        private PayFrequency(string description)
        {
            Description = description;
        }

        public static readonly PayFrequency Weekly = new ("Weekly");
        public static readonly PayFrequency BiWeekly = new ("Bi-Weekly");
        public static readonly PayFrequency SemiMonthly = new ("Semi-Monthly");
        public static readonly PayFrequency Monthly = new("Monthly");

    }
}

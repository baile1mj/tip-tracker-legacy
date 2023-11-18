namespace Core.Models
{
    public class Tip
    {
        public decimal Amount { get; }
        public Server Recipient { get; }
        public DateOnly EarnedOn { get; }
        public Function Function { get; }
        public TipType Type { get; }

        public bool IsFunctionTip => Function != null;

        public Tip(decimal amount, Server recipient, DateOnly earnedOn, TipType type)
        {
            Amount = amount;
            Recipient = recipient;
            EarnedOn = earnedOn;
            Type = type;
        }

        public Tip(decimal amount, Server recipient, Function function, TipType type)
        {
            Amount = amount;
            Recipient = recipient;
            EarnedOn = function.Date;
            Function = function;
            Type = type;
        }

        public Tip(decimal amount, Server recipient, PayPeriod payPeriod, TipType type)
        {
            Amount = amount;
            Recipient = recipient;
            Type = type;
            EarnedOn = type.Determination == DateDetermination.PayPeriodStart
                ? payPeriod.StartDate
                : payPeriod.EndDate;
        }

        public override string ToString()
        {
            return $"{Amount:c} {EarnedOn:d} {Recipient} {Type}";
        }
    }
}

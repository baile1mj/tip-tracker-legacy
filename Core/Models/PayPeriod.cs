namespace Core.Models;

public class PayPeriod
{
    public DateOnly StartDate { get; }

    public DateOnly EndDate { get; }

    public DateOnly CurrentDate { get; }

    public PayPeriod(DateOnly startDate, DateOnly endDate) 
        : this(startDate, endDate, startDate)
    { }

    public PayPeriod(DateOnly startDate, DateOnly endDate, DateOnly currentDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Pay period start cannot fall after the end date.");
        }

        if (currentDate < startDate || currentDate > endDate)
        {
            throw new ArgumentException("Current business date cannot fall outside of pay period.");
        }

        StartDate = startDate;
        EndDate = endDate;
        CurrentDate = currentDate;
    }
}
namespace Bookify.Domain.Bookings;

public record DateRange
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }
    public int LengthInDays => End.DayNumber - Start.DayNumber;

    private DateRange(DateOnly start, DateOnly end)
    {
        Start = start;
        End = end;
    }

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new ApplicationException("End date precedes start date");
        }

        return new(start, end);
    }
}
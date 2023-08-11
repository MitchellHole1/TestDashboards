using TestDashboard.Domain.Services;

namespace TestDashboard.Services;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
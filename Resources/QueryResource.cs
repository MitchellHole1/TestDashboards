namespace TestDashboard.Resources;

public record QueryResource
{
    public int Page { get; init; }
    public int ItemsPerPage { get; init; }
    public string TestType { get; init; }
}
namespace TestDashboard.Domain.Models;

public class TestMedia
{
    public int Id { get; set; }
    public int TestResultId { get; set; }
    public TestResult TestResult { get; set; }
}
namespace TestDashboard.Domain.Models;

public class TestBug
{
    public int Id { get; set; }
    public string Link { get; set; }

    public int TestResultId { get; set; }
    public IList<TestResult> TestResults { get; set; } = new List<TestResult>();
}
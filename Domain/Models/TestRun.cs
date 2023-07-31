namespace TestDashboard.Domain.Models;

public class TestRun
{
    public int Id { get; set; }
    public string Build { get; set; }
    public string Link { get; set; }
    public string TestType { get; set; }
    public int Duration { get; set; }
    public IList<TestResult> TestResults { get; set; } = new List<TestResult>();
}
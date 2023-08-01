namespace TestDashboard.Domain.Models;

public class TestBug
{
    public int Id { get; set; }
    public string Link { get; set; }
    public IList<TestResultBug> TestResultBugs { get; set; } = new List<TestResultBug>();
}
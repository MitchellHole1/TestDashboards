namespace TestDashboard.Domain.Models;

public class TestBug : AuditableEntity
{
    public string Link { get; set; }
    public string Identifier { get; set; }
    public IList<TestResultBug> TestResultBugs { get; set; } = new List<TestResultBug>();
}
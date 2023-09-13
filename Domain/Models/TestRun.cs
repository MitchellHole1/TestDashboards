namespace TestDashboard.Domain.Models;

public class TestRun : AuditableEntity
{ 
    public string Build { get; set; }
    public string Link { get; set; }
    public string TestType { get; set; }
    public double Duration { get; set; }
    public IList<TestResult> TestResults { get; set; } = new List<TestResult>();
}
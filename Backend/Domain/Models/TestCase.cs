namespace TestDashboard.Domain.Models;

public class TestCase : AuditableEntity
{ 
    public string TestName { get; set; }
    public string TestClass { get; set; }
    public IList<TestResult> TestResults { get; set; } = new List<TestResult>();
}
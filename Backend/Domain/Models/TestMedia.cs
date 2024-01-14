namespace TestDashboard.Domain.Models;

public class TestMedia : AuditableEntity
{
    public int TestResultId { get; set; }
    public TestResult TestResult { get; set; }
}
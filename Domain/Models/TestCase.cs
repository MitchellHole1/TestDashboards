namespace TestDashboard.Domain.Models;

public class TestCase
{
    public int Id { get; set; }
    public string TestName { get; set; }
    public string TestClass { get; set; }
    public IList<TestResult> TestResults { get; set; } = new List<TestResult>();
}
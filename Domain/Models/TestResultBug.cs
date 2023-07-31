namespace TestDashboard.Domain.Models;

public class TestResultBug
{
    public int Id { get; set; }
    
    public int TestResultId { get; set; }
    public TestResult TestResult { get; set; }
    
    public int TestBugId { get; set; }
    public TestBug TestBug { get; set; }
}
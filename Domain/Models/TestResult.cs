namespace TestDashboard.Domain.Models;

public class TestResult
{
    public int Id { get; set; }
    public bool Passed { get; set; }
    public int Duration { get; set; }
    
    public int TestRunId { get; set; }
    public TestRun TestRun { get; set; }
    
    public int TestCaseId { get; set; }
    public TestCase TestCase { get; set; }
    
    public IList<TestResultBug> TestResultBugs { get; set; } = new List<TestResultBug>();
    public IList<TestMedia> TestMedia { get; set; } = new List<TestMedia>();
}
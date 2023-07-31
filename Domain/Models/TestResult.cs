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
    
    public IList<TestBug> TestBugs { get; set; } = new List<TestBug>();
    public IList<TestMedia> TestMedia { get; set; } = new List<TestMedia>();
}
using TestDashboard.Domain.Models;

namespace TestDashboard.Resources;

public class TestResultResource
{
    public int Id { get; set; }
    public bool Passed { get; set; }
    public double Duration { get; set; }
    public string? ErrorMessage { get; set; }
    public TestRunResource TestRun { get; set; }
    public TestCaseResource TestCase { get; set; }
    public IList<TestResultBugResource> TestResultBugs { get; set; }
}
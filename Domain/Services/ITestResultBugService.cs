using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services;

public interface ITestResultBugService
{
    Task<TestResultBug> SaveAsync(TestResultBug testResultBug);
}
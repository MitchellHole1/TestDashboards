using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestResultBugRepository
{
    Task AddAsync(TestResultBug testResultBug);
}
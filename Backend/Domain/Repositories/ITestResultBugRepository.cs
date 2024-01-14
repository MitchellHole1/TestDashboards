using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestResultBugRepository
{
    Task AddAsync(TestResultBug testResultBug);
    Task<TestResultBug> FindByIdAsync(int id);

    void Remove(TestResultBug testResultBug);

}
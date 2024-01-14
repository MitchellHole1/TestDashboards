using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestBugRepository
{
    Task<IEnumerable<TestBug>> ListAsync();
    Task AddAsync(TestBug testBug);
}
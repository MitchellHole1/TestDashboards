using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestRunRepository
{
    Task<IEnumerable<TestRun>> ListAsync(string testType);
    Task AddAsync(TestRun testRun);
    Task<TestRun> FindByIdAsync(int id);
    void Update(TestRun testRun);
    void Remove(TestRun testRun);

}
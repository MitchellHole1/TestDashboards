using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestResultRepository
{
    Task<IEnumerable<TestResult>> ListAsync();
    Task AddAsync(TestResult testResult);
    Task<TestResult> FindByIdAsync(int id);
    void Update(TestResult testResult);
    Task<IEnumerable<TestResult>> FindByTestRunId(int testRunId);
    void Remove(TestResult testResult);

}
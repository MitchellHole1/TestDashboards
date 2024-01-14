using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestCaseRepository
{
    Task<IEnumerable<TestCase>> ListAsync(string testType);
    Task AddAsync(TestCase testCase);
    Task<TestCase> FindByIdAsync(int id);
    Task<TestCase> FindByNameAndClassNameAsync(string testName, string className);
    void Update(TestCase testCase);

}
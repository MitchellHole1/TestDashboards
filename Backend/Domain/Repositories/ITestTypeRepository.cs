using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestTypeRepository
{
    Task<IEnumerable<TestType>> ListAsync();
    Task<TestType> FindByNameAsync(string name);
    Task AddAsync(TestType testType);
}
using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestTypeRepository
{
    Task<IEnumerable<TestType>> ListAsync();
}
using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services;

public interface ITestTypeService
{
    Task<IEnumerable<TestType>> ListAsync();
}
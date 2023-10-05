using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Domain.Services;

public interface ITestTypeService
{
    Task<IEnumerable<TestType>> ListAsync();
    Task<SaveTestTypeResponse> SaveAsync(TestType testType);
    Task<SaveTestTypeResponse> GetByNameAsync(string name);
}
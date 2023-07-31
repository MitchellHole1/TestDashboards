using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;
using TestDashboard.Resources;

namespace TestDashboard.Domain.Services;

public interface ITestCaseService
{
    Task<IEnumerable<TestCase>> ListAsync();
    Task<SaveTestCaseResponse> SaveAsync(TestCase testCase);
    Task<SaveTestCaseResponse> UpdateAsync(int id, TestCase testCase);

}
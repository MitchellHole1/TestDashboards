using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;
using TestDashboard.Resources;

namespace TestDashboard.Domain.Services;

public interface ITestRunService
{
    Task<IEnumerable<TestRun>> ListAsync();
    Task<SaveTestRunResponse> SaveAsync(TestRun testRun);
    Task<SaveTestRunResponse> UpdateAsync(int id, TestRun testRun);
}
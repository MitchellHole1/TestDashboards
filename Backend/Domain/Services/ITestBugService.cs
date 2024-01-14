using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Domain.Services;

public interface ITestBugService
{
    Task<IEnumerable<TestBug>> ListAsync();
    Task<SaveTestBugResponse> SaveAsync(TestBug testBug);
    
}
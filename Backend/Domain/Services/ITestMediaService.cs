using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services;

public interface ITestMediaService
{
    Task<IEnumerable<TestMedia>> ListAsync();

}
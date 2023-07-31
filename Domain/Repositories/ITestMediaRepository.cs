using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Repositories;

public interface ITestMediaRepository
{
    Task<IEnumerable<TestMedia>> ListAsync();

}
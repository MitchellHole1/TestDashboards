using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;

namespace TestDashboard.Services;

public class TestMediaService : ITestMediaService
{
    private readonly ITestMediaRepository _testMediaRepository;
    
    public TestMediaService(ITestMediaRepository testMediaRepository)
    {
        this._testMediaRepository = testMediaRepository;
    }
    
    public async Task<IEnumerable<TestMedia>> ListAsync()
    {
        return await _testMediaRepository.ListAsync();
    }
}
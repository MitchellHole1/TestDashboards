using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;

namespace TestDashboard.Services;

public class TestTypeService : ITestTypeService
{
    private readonly ITestTypeRepository _testTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TestTypeService(ITestTypeRepository testTypeRepository, IUnitOfWork unitOfWork)
    {
        _testTypeRepository = testTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TestType>> ListAsync()
    {
        return await _testTypeRepository.ListAsync();
    }
}
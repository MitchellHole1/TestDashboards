using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

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
    
    public async Task<SaveTestTypeResponse> GetByNameAsync(string name)
    {
        var TestType = await _testTypeRepository.FindByNameAsync(name);

        if (TestType == null)
            return new SaveTestTypeResponse("Testtype not found.");
        
        return new SaveTestTypeResponse(TestType);
    }
    
    public async Task<SaveTestTypeResponse> SaveAsync(TestType testType)
    {
        try
        {
            await _testTypeRepository.AddAsync(testType);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestTypeResponse(testType);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestTypeResponse($"An error occurred when saving the testtype: {ex.Message}");
        }
    }
}
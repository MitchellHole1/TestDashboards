using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Services;

public class TestBugService : ITestBugService
{
    private readonly ITestBugRepository _testBugRepository;
    private readonly ITestResultRepository _testResultRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public TestBugService(ITestBugRepository testBugRepository, ITestResultRepository testResultRepository, IUnitOfWork unitOfWork)
    {
        _testBugRepository = testBugRepository;
        _testResultRepository = testResultRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<TestBug>> ListAsync()
    {
        return await _testBugRepository.ListAsync();
    }
    
    public async Task<SaveTestBugResponse> SaveAsync(TestBug testBug)
    {
        try
        {
            var existingTestResult = await _testResultRepository.FindByIdAsync(testBug.TestResultId);
            if (existingTestResult == null)
                return new SaveTestBugResponse("Invalid testresult.");
            
            await _testBugRepository.AddAsync(testBug);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestBugResponse(testBug);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestBugResponse($"An error occurred when saving the testbug: {ex.Message}");
        }
    }
}
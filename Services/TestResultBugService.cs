using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;

namespace TestDashboard.Services;

public class TestResultBugService : ITestResultBugService
{
    private readonly ITestResultBugRepository _testResultBugRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public TestResultBugService(ITestResultBugRepository testResultBugRepository, IUnitOfWork unitOfWork)
    {
        _testResultBugRepository = testResultBugRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TestResultBug> SaveAsync(TestResultBug testResultBug)
    {
        await _testResultBugRepository.AddAsync(testResultBug);
        await _unitOfWork.CompleteAsync();
			
        return testResultBug;
    }
}
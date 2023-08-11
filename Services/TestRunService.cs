using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;
using TestDashboard.Resources;

namespace TestDashboard.Services;

public class TestRunService : ITestRunService
{
    private readonly ITestRunRepository _testRunRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TestRunService(ITestRunRepository testRunRepository, IUnitOfWork unitOfWork)
    {
        _testRunRepository = testRunRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TestRun>> ListAsync(Query q)
    {
        return await _testRunRepository.ListAsync();
    }
    
    public async Task<SaveTestRunResponse> SaveAsync(TestRun testRun)
    {
        try
        {
            await _testRunRepository.AddAsync(testRun);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestRunResponse(testRun);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestRunResponse($"An error occurred when saving the testrun: {ex.Message}");
        }
    }
    
    public async Task<SaveTestRunResponse> UpdateAsync(int id, TestRun testRun)
    {
        var existingTestRun = await _testRunRepository.FindByIdAsync(id);

        if (existingTestRun == null)
            return new SaveTestRunResponse("Testrun not found.");

        existingTestRun.Build = testRun.Build;
        existingTestRun.Link = testRun.Link;
        existingTestRun.Duration = testRun.Duration;
        existingTestRun.TestType = testRun.TestType;

        try
        {
            _testRunRepository.Update(existingTestRun);
            await _unitOfWork.CompleteAsync();

            return new SaveTestRunResponse(existingTestRun);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestRunResponse($"An error occurred when updating the testrun: {ex.Message}");
        }
    }
}
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Services;

public class TestResultService : ITestResultService
{
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestRunRepository _testRunRepository;
    private readonly ITestCaseRepository _testCaseRepository;

    private readonly IUnitOfWork _unitOfWork;

    public TestResultService(ITestResultRepository testResultRepository, ITestRunRepository testRunRepository,
        ITestCaseRepository testCaseRepository, IUnitOfWork unitOfWork)
    {
        _testResultRepository = testResultRepository;
        _testRunRepository = testRunRepository;
        _testCaseRepository = testCaseRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<TestResult>> ListAsync()
    {
        return await _testResultRepository.ListAsync();
    }
    
    public async Task<SaveTestResultResponse> SaveAsync(TestResult testResult)
    {
        try
        {
            var existingTestRun = await _testRunRepository.FindByIdAsync(testResult.TestRunId);
            if (existingTestRun == null)
                return new SaveTestResultResponse("Invalid testrun.");
            
            var existingTestCase = await _testCaseRepository.FindByIdAsync(testResult.TestCaseId);
            if (existingTestCase == null)
                return new SaveTestResultResponse("Invalid testcase.");
            
            await _testResultRepository.AddAsync(testResult);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestResultResponse(testResult);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestResultResponse($"An error occurred when saving the testresult: {ex.Message}");
        }
    }
    
    public async Task<SaveTestResultResponse> UpdateAsync(int id, TestResult testResult)
    {
        var existingTestResult = await _testResultRepository.FindByIdAsync(id);

        if (existingTestResult == null)
            return new SaveTestResultResponse("Testresult not found.");
        
        var existingTestRun = await _testRunRepository.FindByIdAsync(testResult.TestRunId);
        if (existingTestRun == null)
            return new SaveTestResultResponse("Invalid testrun.");
            
        var existingTestCase = await _testCaseRepository.FindByIdAsync(testResult.TestCaseId);
        if (existingTestCase == null)
            return new SaveTestResultResponse("Invalid testcase.");

        existingTestResult.Duration = testResult.Duration;
        existingTestResult.Passed = testResult.Passed;

        try
        {
            _testResultRepository.Update(existingTestResult);
            await _unitOfWork.CompleteAsync();

            return new SaveTestResultResponse(existingTestResult);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestResultResponse($"An error occurred when updating the testesult: {ex.Message}");
        }
    }
    
    public async Task<SaveTestResultResponse> DeleteAsync(int id)
    {
        var existingCategory = await _testResultRepository.FindByIdAsync(id);

        if (existingCategory == null)
            return new SaveTestResultResponse("Testresult not found.");

        try
        {
            _testResultRepository.Remove(existingCategory);
            await _unitOfWork.CompleteAsync();

            return new SaveTestResultResponse(existingCategory);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestResultResponse($"An error occurred when deleting the testresult: {ex.Message}");
        }
    }
    
    public async Task<IEnumerable<TestResult>> ListByTestRunAsync(int id)
    {
        return await _testResultRepository.FindByTestRunId(id);
    }
}
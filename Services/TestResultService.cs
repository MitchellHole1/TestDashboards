using System.Xml.Linq;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Services;

public class TestResultService : ITestResultService
{
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestResultBugService _testResultBugService;
    private readonly ITestRunRepository _testRunRepository;
    private readonly ITestCaseRepository _testCaseRepository;

    private readonly IUnitOfWork _unitOfWork;

    public TestResultService(ITestResultRepository testResultRepository, ITestRunRepository testRunRepository,
        ITestCaseRepository testCaseRepository, ITestResultBugService testResultBugService, IUnitOfWork unitOfWork)
    {
        _testResultRepository = testResultRepository;
        _testRunRepository = testRunRepository;
        _testCaseRepository = testCaseRepository;
        _testResultBugService = testResultBugService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<TestResult>> ListAsync()
    {
        return await _testResultRepository.ListAsync();
    }
    
    public async Task<SaveTestResultResponse> GetByIdAsync(int id)
    {
        var existingTestResult = await _testResultRepository.FindByIdAsync(id);

        if (existingTestResult == null)
            return new SaveTestResultResponse("Testresult not found.");
        
        return new SaveTestResultResponse(existingTestResult);
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

    public async Task<SaveTestResultResponse> SaveTestResultBugAsync(TestResultBug testResultBug)
    {
        try
        {
            var existingTestResult = await _testResultRepository.FindByIdAsync(testResultBug.TestResultId);
            if (existingTestResult == null)
                return new SaveTestResultResponse("Invalid testresult.");

            await _testResultBugService.SaveAsync(testResultBug);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestResultResponse(existingTestResult);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestResultResponse($"An error occurred when adding the testbug: {ex.Message}");
        }
    }
    
    public async Task<SaveTestResultResponse> DeleteTestResultBugAsync(int testResultId, int testBugId)
    {
        try
        {
            var existingTestResult = await _testResultRepository.FindByIdAsync(testResultId);
            if (existingTestResult == null)
                return new SaveTestResultResponse("Invalid testresult.");
            
            TestResultBug testResultBug = await _testResultBugService.DeleteAsync(testBugId);
            if (testResultBug == null)
                return new SaveTestResultResponse("Invalid testresultbug.");
            return new SaveTestResultResponse(existingTestResult);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestResultResponse($"An error occurred when adding the testbug: {ex.Message}");
        }
    }

    public async Task<SaveTestResultsResponse>  UploadResults(int id, XElement testResults)
    {
        var results = testResults.Descendants("testcase");
        List<TestResult> savedResults = new List<TestResult>();
        try {
            foreach (var result in results) { 
                var temp = new TestResult
                {
                    Duration = (int) float.Parse(result.Attribute("time")!.Value),
                    Passed = !result.Descendants("failure").Any(),
                    TestRunId = id
                };
                var className = result.Attribute("classname")!.Value;
                var testName = result.Attribute("name")!.Value;
                testName = testName.Replace(className + ".", "");

                var testCase = await _testCaseRepository.FindByNameAndClassNameAsync(testName, className);
                if (testCase != null)
                {
                    temp.TestCaseId = testCase.Id;
                    savedResults.Add(temp);
                }
                else
                {
                    return new SaveTestResultsResponse("Invalid testcase: " + className + "-" + testName);
                }
            }

            await _testResultRepository.AddRangeAsync(savedResults);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            return new SaveTestResultsResponse($"An error occurred when uploading the testresults: {ex.Message}");
        }
        return new SaveTestResultsResponse(savedResults);
    }

}
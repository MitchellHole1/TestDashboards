using System.Xml.Linq;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Services;

public class TestRunService : ITestRunService
{
    private readonly ITestRunRepository _testRunRepository;
    private readonly ITestResultService _testResultService;
    private readonly IUnitOfWork _unitOfWork;

    public TestRunService(ITestRunRepository testRunRepository, ITestResultService testResultService, IUnitOfWork unitOfWork)
    {
        _testRunRepository = testRunRepository;
        _testResultService = testResultService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TestRun>> ListAsync(Query q)
    {
        return await _testRunRepository.ListAsync(q.TestType);
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

    public async Task<SaveTestRunResponse> UploadResults(int id, XElement testResults)
    {
        var existingTestRun = await _testRunRepository.FindByIdAsync(id);

        if (existingTestRun == null)
            return new SaveTestRunResponse("Testrun not found.");

        try
        {
            existingTestRun.Duration = double.Parse(testResults.Attributes("time").FirstOrDefault().Value);
            _testRunRepository.Update(existingTestRun);
            var saveTestResultsResponse = await _testResultService.UploadResults(id, testResults);
            if (!saveTestResultsResponse.Success)
            {
                return new SaveTestRunResponse("Failed uploading results: " + saveTestResultsResponse.Message);
            }
            await _unitOfWork.CompleteAsync();
            return new SaveTestRunResponse(existingTestRun);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestRunResponse($"An error occurred when uploading testresults: {ex.Message}");
        }
    }

}
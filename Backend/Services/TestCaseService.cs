using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Services;

public class TestCaseService : ITestCaseService
{
    private readonly ITestCaseRepository _testCaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TestCaseService(ITestCaseRepository testCaseRepository, IUnitOfWork unitOfWork)
    {
        _testCaseRepository = testCaseRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<TestCase>> ListAsync(Query q)
    {
        return await _testCaseRepository.ListAsync(q.TestType);
    }
    
    public async Task<SaveTestCaseResponse> SaveAsync(TestCase testCase)
    {
        try
        {
            await _testCaseRepository.AddAsync(testCase);
            await _unitOfWork.CompleteAsync();
			
            return new SaveTestCaseResponse(testCase);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestCaseResponse($"An error occurred when saving the testcase: {ex.Message}");
        }
    }
    
    public async Task<SaveTestCaseResponse> UpdateAsync(int id, TestCase testCase)
    {
        var existingTestCase = await _testCaseRepository.FindByIdAsync(id);

        if (existingTestCase == null)
            return new SaveTestCaseResponse("Testcase not found.");

        existingTestCase.TestName = testCase.TestName;
        existingTestCase.TestClass = testCase.TestClass;

        try
        {
            _testCaseRepository.Update(existingTestCase);
            await _unitOfWork.CompleteAsync();

            return new SaveTestCaseResponse(existingTestCase);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new SaveTestCaseResponse($"An error occurred when updating the testcase: {ex.Message}");
        }
    }
}
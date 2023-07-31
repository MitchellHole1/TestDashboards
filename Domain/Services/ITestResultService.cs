using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Domain.Services;

public interface ITestResultService
{
    Task<IEnumerable<TestResult>> ListAsync();
    Task<SaveTestResultResponse> SaveAsync(TestResult testResult);
    Task<SaveTestResultResponse> UpdateAsync(int id, TestResult testResult);
    Task<IEnumerable<TestResult>> ListByTestRunAsync(int id);
    Task<SaveTestResultResponse> DeleteAsync(int id);

}
using System.Xml.Linq;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;

namespace TestDashboard.Domain.Services;

public interface ITestResultService
{
    Task<IEnumerable<TestResult>> ListAsync();
    Task<SaveTestResultResponse> GetByIdAsync(int id);
    Task<SaveTestResultResponse> SaveAsync(TestResult testResult);
    Task<SaveTestResultResponse> UpdateAsync(int id, TestResult testResult);
    Task<IEnumerable<TestResult>> ListByTestRunAsync(int id);
    Task<SaveTestResultResponse> DeleteAsync(int id);
    Task<SaveTestResultResponse> SaveTestResultBugAsync(TestResultBug testResultBug);
    Task<SaveTestResultResponse>  DeleteTestResultBugAsync(int testResultId, int testBugId);
    Task<SaveTestResultsResponse>  UploadResults(int testRunId, XElement testResults);
}
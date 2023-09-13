using System.Xml.Linq;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services.Communication;
using TestDashboard.Resources;

namespace TestDashboard.Domain.Services;

public interface ITestRunService
{
    Task<IEnumerable<TestRun>> ListAsync(Query q);
    Task<SaveTestRunResponse> SaveAsync(TestRun testRun);
    Task<SaveTestRunResponse> UpdateAsync(int id, TestRun testRun);
    Task<SaveTestRunResponse> UploadResults(int id, XElement testResults);
}
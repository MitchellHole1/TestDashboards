using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestResultsResponse : BaseResponse
{
    public List<TestResult> TestResults { get; private set; }

    private SaveTestResultsResponse(bool success, string message, List<TestResult> testResults) : base(success, message)
    {
        TestResults = testResults;
    }
    
    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="testresults">Saved testresults.</param>
    /// <returns>Response.</returns>
    public SaveTestResultsResponse(List<TestResult> testResults) : this(true, string.Empty, testResults)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestResultsResponse(string message) : this(false, message, null)
    { }
}
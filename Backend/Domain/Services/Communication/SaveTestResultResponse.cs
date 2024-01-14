using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestResultResponse : BaseResponse
{
    public TestResult TestResult { get; private set; }
    
    private SaveTestResultResponse(bool success, string message, TestResult testResult) : base(success, message)
    {
        TestResult = testResult;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="category">Saved category.</param>
    /// <returns>Response.</returns>
    public SaveTestResultResponse(TestResult testResult) : this(true, string.Empty, testResult)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestResultResponse(string message) : this(false, message, null)
    { }
}
using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestCaseResponse : BaseResponse
{
    public TestCase TestCase { get; private set; }
    
    private SaveTestCaseResponse(bool success, string message, TestCase testCase) : base(success, message)
    {
        TestCase = testCase;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="category">Saved category.</param>
    /// <returns>Response.</returns>
    public SaveTestCaseResponse(TestCase testCase) : this(true, string.Empty, testCase)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestCaseResponse(string message) : this(false, message, null)
    { }
}
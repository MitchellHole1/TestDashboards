using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestRunResponse : BaseResponse
{
    public TestRun TestRun { get; private set; }
    
    private SaveTestRunResponse(bool success, string message, TestRun testRun) : base(success, message)
    {
        TestRun = testRun;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="category">Saved category.</param>
    /// <returns>Response.</returns>
    public SaveTestRunResponse(TestRun testRun) : this(true, string.Empty, testRun)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestRunResponse(string message) : this(false, message, null)
    { }
    
}
using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestBugResponse : BaseResponse
{
    public TestBug TestBug { get; private set; }
    
    private SaveTestBugResponse(bool success, string message, TestBug testBug) : base(success, message)
    {
        TestBug = testBug;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="testbug">Saved testbug.</param>
    /// <returns>Response.</returns>
    public SaveTestBugResponse(TestBug testBug) : this(true, string.Empty, testBug)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestBugResponse(string message) : this(false, message, null)
    { }
}
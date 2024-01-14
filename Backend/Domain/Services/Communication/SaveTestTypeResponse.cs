using TestDashboard.Domain.Models;

namespace TestDashboard.Domain.Services.Communication;

public class SaveTestTypeResponse : BaseResponse
{
    public TestType TestType { get; private set; }
    
    private SaveTestTypeResponse(bool success, string message, TestType testType) : base(success, message)
    {
        TestType = testType;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="category">Saved TestType.</param>
    /// <returns>Response.</returns>
    public SaveTestTypeResponse(TestType testType) : this(true, string.Empty, testType)
    { }

    /// <summary>
    /// Creates am error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>Response.</returns>
    public SaveTestTypeResponse(string message) : this(false, message, null)
    { }
}
using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestResultResource
{
    [Required]
    public bool Passed { get; set; }
    
    [Required]
    public int TestRunId { get; init; }
    
    [Required]
    public int TestCaseId { get; init; }
    
    public int Duration { get; set; }
}
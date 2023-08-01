using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestBugResource
{
    [Required]
    public String Link { get; set; }
    
    [Required]
    public int TestResultId { get; init; }
}
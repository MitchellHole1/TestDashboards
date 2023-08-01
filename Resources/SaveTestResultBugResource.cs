using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestResultBugResource
{
    [Required]
    public int TestBugId { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestResultResource
{
    [Required]
    public bool Passed { get; set; }

    public int Duration { get; set; }
}
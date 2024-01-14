using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestRunResource
{
    [Required]
    [MaxLength(30)]
    public string Build { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Link { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string TestTypeName { get; set; }
    
    public double Duration { get; set; }
}
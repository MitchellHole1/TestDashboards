using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestBugResource
{
    [Required]
    public String Link { get; set; }
    
    public string Identifier { get; set; }
}
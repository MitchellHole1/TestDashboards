using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestCaseResource
{
    [Required]
    [MaxLength(100)]
    public string TestName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string TestClass { get; set; }
}
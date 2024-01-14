using System.ComponentModel.DataAnnotations;

namespace TestDashboard.Resources;

public class SaveTestTypeResource
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}
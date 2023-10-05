namespace TestDashboard.Resources;

public class TestRunResource
{
    public int Id { get; set; }
    public string Build { get; set; }
    public string Link { get; set; }
    public string TestTypeName { get; set; }
    public double Duration { get; set; }
    public DateTime Created { get; set; }
}
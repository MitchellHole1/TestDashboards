namespace TestDashboard.Domain.Models;

public class Query
{
    public int Page { get; protected set; }
    public int ItemsPerPage { get; protected set; }
    
    public string TestType { get; protected set; }

    public Query(int page, int itemsPerPage, string testType)
    {
        Page = page;
        ItemsPerPage = itemsPerPage;
        TestType = testType;

        if (Page <= 0)
        {
            Page = 1;
        }

        if (ItemsPerPage <= 0)
        {
            ItemsPerPage = 10;
        }
    }
}
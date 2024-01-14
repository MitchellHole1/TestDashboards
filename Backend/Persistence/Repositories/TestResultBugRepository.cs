using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestResultBugRepository : BaseRepository, ITestResultBugRepository
{
    public TestResultBugRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task AddAsync(TestResultBug testResultBug)
    {
        await _context.TestResultBugs.AddAsync(testResultBug);
    }
    
    public async Task<TestResultBug> FindByIdAsync(int id)
    {
        return await _context.TestResultBugs.FindAsync(id);
    }

    public void Remove(TestResultBug testResultBug)
    {
        _context.TestResultBugs.Remove(testResultBug);
    }
}
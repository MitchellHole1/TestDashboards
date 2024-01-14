using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestBugRepository: BaseRepository, ITestBugRepository
{
    public TestBugRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestBug>> ListAsync()
    {
        return await _context.TestBugs.Include(p => p.TestResultBugs).ToListAsync();
    }
    
    public async Task AddAsync(TestBug testBug)
    {
        await _context.TestBugs.AddAsync(testBug);
    }
}
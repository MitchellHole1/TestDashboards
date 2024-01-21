using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestRunRepository: BaseRepository, ITestRunRepository
{
    public TestRunRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestRun>> ListAsync(string testType)
    {
        return await _context.TestRuns.Where(p => p.TestTypeName == testType || testType == null).OrderByDescending(i => i.Id).Take(20).ToListAsync();
    }
    
    public async Task<TestRun> FindByIdAsync(int id)
    {
        return await _context.TestRuns.FindAsync(id);
    }

    public async Task AddAsync(TestRun testRun)
    {
        await _context.TestRuns.AddAsync(testRun);
    }
    
    public void Update(TestRun testRun)
    {
        _context.TestRuns.Update(testRun);
    }
    
    public void Remove(TestRun testRun)
    {
        _context.TestRuns.Remove(testRun);
    }
}
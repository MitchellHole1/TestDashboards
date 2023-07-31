using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestResultRepository: BaseRepository, ITestResultRepository
{
    public TestResultRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestResult>> ListAsync()
    {
        return await _context.TestResults.Include(p => p.TestBugs).Include(p => p.TestRun).Include(p => p.TestCase).ToListAsync();
    }
    
    public async Task AddAsync(TestResult testResult)
    {
        await _context.TestResults.AddAsync(testResult);
    }
    
    public async Task<TestResult> FindByIdAsync(int id)
    {
        return await _context.TestResults.FindAsync(id);
    }
    
    public void Update(TestResult testResult)
    {
        _context.TestResults.Update(testResult);
    }
    
    public async Task<IEnumerable<TestResult>> FindByTestRunId(int testRunId)
    {
        return await _context.TestResults.Where(p => p.TestRun.Id == testRunId).Include(p => p.TestCase).ToListAsync();
    }
    
    public void Remove(TestResult testResult)
    {
        _context.TestResults.Remove(testResult);
    }
}
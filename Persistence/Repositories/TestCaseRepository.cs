using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestCaseRepository: BaseRepository, ITestCaseRepository
{
    public TestCaseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestCase>> ListAsync(string testType)
    {
        return await _context.TestCases.ToListAsync();
    }
    
    public async Task<TestCase> FindByIdAsync(int id)
    {
        return await _context.TestCases.FindAsync(id);
    }
    
    public async Task<TestCase> FindByNameAndClassNameAsync(string testName, string className)
    {
        return await _context.TestCases.Where(p => p.TestName == testName && p.TestClass == className).FirstOrDefaultAsync();
    }
    
    public async Task AddAsync(TestCase testCase)
    {
        await _context.TestCases.AddAsync(testCase);
    }
    
    public void Update(TestCase testCase)
    {
        _context.TestCases.Update(testCase);
    }}
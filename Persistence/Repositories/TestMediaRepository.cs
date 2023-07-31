using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestMediaRepository: BaseRepository, ITestMediaRepository
{
    public TestMediaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestMedia>> ListAsync()
    {
        return await _context.TestMedia.ToListAsync();
    }
}
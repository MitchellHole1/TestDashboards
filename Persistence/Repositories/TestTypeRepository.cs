using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Persistence.Contexts;

namespace TestDashboard.Persistence.Repositories;

public class TestTypeRepository : BaseRepository, ITestTypeRepository
{
    public TestTypeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TestType>> ListAsync()
    {
        return await _context.TestTypes.OrderBy(i => i.Id).ToListAsync();
    }

}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Resources;

namespace TestDashboard.Controllers;

[Route("/api/[controller]")]
public class TestTypeController : Controller
{
    private readonly ITestTypeService _testTypeService;
    private readonly IMapper _mapper;
    
    public TestTypeController(ITestTypeService testTypeService, IMapper mapper)
    {
        _testTypeService = testTypeService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TestTypeResource>> GetAllAsync()
    {
        var testTypes = await (_testTypeService).ListAsync();
        var resources = _mapper.Map<IEnumerable<TestType>, IEnumerable<TestTypeResource>>(testTypes);

        return resources;
    }
}
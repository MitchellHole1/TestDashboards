using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services;
using TestDashboard.Extensions;
using TestDashboard.Resources;

namespace TestDashboard.Controllers;

[Route("/api/[controller]")]
public class TestBugController : Controller
{
    private readonly ITestBugService _testBugService;
    private readonly IMapper _mapper;


    public TestBugController(ITestBugService testBugService, IMapper mapper)
    {
        _testBugService = testBugService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TestBugResource>> GetAllAsync()
    {
        var testBugs = await _testBugService.ListAsync();
        var resources = _mapper.Map<IEnumerable<TestBug>, IEnumerable<TestBugResource>>(testBugs);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTestBugResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testBug = _mapper.Map<TestBug>(resource);
        var result = await _testBugService.SaveAsync(testBug);

        if (!result.Success)
            return BadRequest(result.Message);

        var testBugResource = _mapper.Map<TestBug, TestBugResource>(result.TestBug);
        return Ok(testBugResource);
    }
}
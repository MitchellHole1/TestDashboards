using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services;
using TestDashboard.Extensions;
using TestDashboard.Resources;

namespace TestDashboard.Controllers;

[Route("/api/[controller]")]
public class TestCaseController : Controller
{
    private readonly ITestCaseService _testCaseService;
    private readonly IMapper _mapper;

    public TestCaseController(ITestCaseService testCaseService, IMapper mapper)
    {
        _testCaseService = testCaseService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TestCaseResource>> GetAllAsync()
    {
        var testCases = await _testCaseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<TestCase>, IEnumerable<TestCaseResource>>(testCases);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTestCaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testCase = _mapper.Map<SaveTestCaseResource, TestCase>(resource);
        var result = await _testCaseService.SaveAsync(testCase);

        if (!result.Success)
            return BadRequest(result.Message);

        var testCaseResource = _mapper.Map<TestCase, TestCaseResource>(result.TestCase);
        return Ok(testCaseResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTestCaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var testCase = _mapper.Map<SaveTestCaseResource, TestCase>(resource);
        var result = await _testCaseService.UpdateAsync(id, testCase);

        if (!result.Success)
            return BadRequest(result.Message);

        var testCaseResource = _mapper.Map<TestCase, TestCaseResource>(result.TestCase);
        return Ok(testCaseResource);
    }
}
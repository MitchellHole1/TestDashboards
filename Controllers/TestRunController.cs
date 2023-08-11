using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services;
using TestDashboard.Extensions;
using TestDashboard.Resources;

namespace TestDashboard.Controllers;

[Route("/api/[controller]")]
public class TestRunController : Controller
{
    private readonly ITestRunService _testRunService;
    private readonly ITestResultService _testResultService;
    private readonly IMapper _mapper;


    public TestRunController(ITestRunService testRunService, ITestResultService testResultService, IMapper mapper)
    {
        _testRunService = testRunService;
        _testResultService = testResultService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TestRunResource>> GetAllAsync([FromQuery] QueryResource testRunQuery)
    {
        var query = _mapper.Map<Query>(testRunQuery);

        var testRuns = await _testRunService.ListAsync(query);
        var resources = _mapper.Map<IEnumerable<TestRun>, IEnumerable<TestRunResource>>(testRuns);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTestRunResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testRun = _mapper.Map<SaveTestRunResource, TestRun>(resource);
        var result = await _testRunService.SaveAsync(testRun);

        if (!result.Success)
            return BadRequest(result.Message);

        var testRunResource = _mapper.Map<TestRun, TestRunResource>(result.TestRun);
        return Ok(testRunResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTestRunResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var testRun = _mapper.Map<SaveTestRunResource, TestRun>(resource);
        var result = await _testRunService.UpdateAsync(id, testRun);

        if (!result.Success)
            return BadRequest(result.Message);

        var testRunResource = _mapper.Map<TestRun, TestRunResource>(result.TestRun);
        return Ok(testRunResource);
    }
    
    [HttpGet("{id}/testresults")]
    public async Task<IEnumerable<TestResultResource>> GetTestResultsByTestRunAsync(int id)
    {
        var testResults = await _testResultService.ListByTestRunAsync(id);
        var resources = _mapper.Map<IEnumerable<TestResult>, IEnumerable<TestResultResource>>(testResults);

        return resources;
    }
}
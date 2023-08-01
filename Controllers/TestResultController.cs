using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Services;
using TestDashboard.Extensions;
using TestDashboard.Resources;

namespace TestDashboard.Controllers;

[Route("/api/[controller]")]
public class TestResultController : Controller
{
    private readonly ITestResultService _testResultService;
    private readonly IMapper _mapper;


    public TestResultController(ITestResultService testResultService, IMapper mapper)
    {
        _testResultService = testResultService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TestResultResource>> GetAllAsync()
    {
        var testResults = await _testResultService.ListAsync();
        var resources = _mapper.Map<IEnumerable<TestResult>, IEnumerable<TestResultResource>>(testResults);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTestResultResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testResult = _mapper.Map<TestResult>(resource);
        var result = await _testResultService.SaveAsync(testResult);

        if (!result.Success)
            return BadRequest(result.Message);

        var testResultResource = _mapper.Map<TestResult, TestResultResource>(result.TestResult);
        return Ok(testResultResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTestResultResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var testResult = _mapper.Map<SaveTestResultResource, TestResult>(resource);
        var result = await _testResultService.UpdateAsync(id, testResult);

        if (!result.Success)
            return BadRequest(result.Message);

        var testResultResource = _mapper.Map<TestResult, TestResultResource>(result.TestResult);
        return Ok(testResultResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _testResultService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        return NoContent();
    }
    
    [HttpPost("{id}/testbug")]
    public async Task<IActionResult> PostTestBugAsync(int id, [FromBody] SaveTestResultBugResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testResultBug = _mapper.Map<TestResultBug>(resource);
        testResultBug.TestResultId = id;
        var result = await _testResultService.SaveTestResultBugAsync(testResultBug);

        if (!result.Success)
            return BadRequest(result.Message);

        var testResultResource = _mapper.Map<TestResult, TestResultResource>(result.TestResult);
        return Ok(testResultResource);
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestDashboard.Domain.Models;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Extensions;
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
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTestTypeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var testType = _mapper.Map<SaveTestTypeResource, TestType>(resource);
        var result = await _testTypeService.SaveAsync(testType);

        if (!result.Success)
            return BadRequest(result.Message);

        var testRunResource = _mapper.Map<TestType, TestTypeResource>(result.TestType);
        return Ok(testRunResource);
    }
}
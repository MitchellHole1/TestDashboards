using AutoMapper;
using TestDashboard.Domain.Models;
using TestDashboard.Resources;

namespace SuperMarket.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveTestRunResource, TestRun>();
        CreateMap<SaveTestCaseResource, TestCase>();
        CreateMap<SaveTestResultResource, TestResult>();
        CreateMap<SaveTestBugResource, TestBug>();
        CreateMap<SaveTestResultBugResource, TestResultBug>();
        CreateMap<QueryResource, Query>();
    }  
}
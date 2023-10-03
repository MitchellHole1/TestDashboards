using AutoMapper;
using TestDashboard.Domain.Models;
using TestDashboard.Persistence.Repositories;
using TestDashboard.Resources;

namespace SuperMarket.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<TestRun, TestRunResource>();
        CreateMap<TestCase, TestCaseResource>();
        CreateMap<TestResult, TestResultResource>();
        CreateMap<TestBug, TestBugResource>();
        CreateMap<TestResultBug, TestResultBugResource>();
        CreateMap<TestType, TestTypeResource>();
    }
}
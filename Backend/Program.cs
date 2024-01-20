using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestDashboard.Domain.Repositories;
using TestDashboard.Domain.Services;
using TestDashboard.Persistence.Contexts;
using TestDashboard.Persistence.Interceptors;
using TestDashboard.Persistence.Repositories;
using TestDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    if (!builder.Environment.IsEnvironment("Local"))
    {
        options.UseInMemoryDatabase("testdashboard-api-in-memory");
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("Postgres");
        options.UseNpgsql(connectionString);
    }
    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
});

builder.Services.AddScoped<ITestRunRepository, TestRunRepository>();
builder.Services.AddScoped<ITestRunService, TestRunService>();
builder.Services.AddScoped<ITestCaseRepository, TestCaseRepository>();
builder.Services.AddScoped<ITestCaseService, TestCaseService>();
builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();
builder.Services.AddScoped<ITestResultService, TestResultService>();
builder.Services.AddScoped<ITestBugRepository, TestBugRepository>();
builder.Services.AddScoped<ITestBugService, TestBugService>();
builder.Services.AddScoped<ITestResultBugRepository, TestResultBugRepository>();
builder.Services.AddScoped<ITestResultBugService, TestResultBugService>();
builder.Services.AddScoped<ITestMediaRepository, TestMediaRepository>();
builder.Services.AddScoped<ITestMediaService, TestMediaService>();
builder.Services.AddScoped<ITestTypeRepository, TestTypeRepository>();
builder.Services.AddScoped<ITestTypeService, TestTypeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
builder.Services.AddTransient<IDateTime, DateTimeService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using(var scope = app.Services.CreateScope())
using(var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;

namespace TestDashboard.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<TestType> TestTypes { get; set; }
    public DbSet<TestRun> TestRuns { get; set; }
    public DbSet<TestCase> TestCases { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<TestBug> TestBugs { get; set; }
    public DbSet<TestResultBug> TestResultBugs { get; set; }
    public DbSet<TestMedia> TestMedia { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TestType>().ToTable("TestType");
        builder.Entity<TestType>().HasKey(p => p.Id);
        builder.Entity<TestType>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestType>().Property(p => p.Name).IsRequired().HasMaxLength(15);
        builder.Entity<TestType>().HasIndex(p => p.Name).IsUnique();

        builder.Entity<TestType>().HasData(
            new TestType { Id = 1, Name = "API"},
            new TestType { Id = 2, Name = "UI"}
        );
        
        builder.Entity<TestRun>().ToTable("TestRuns");
        builder.Entity<TestRun>().HasKey(p => p.Id);
        builder.Entity<TestRun>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestRun>().Property(p => p.Build).IsRequired().HasMaxLength(15);
        builder.Entity<TestRun>().HasIndex(p => p.Build).IsUnique();
        builder.Entity<TestRun>().HasMany(p => p.TestResults).WithOne(p => p.TestRun).HasForeignKey(p => p.TestRunId);
        
        builder.Entity<TestRun>().HasData
        (
            new TestRun { Id = 1, Build = "abcd123", TestTypeName = "API", Duration = 53.12, Link = "test.com", Created = new DateTime(2023, 9, 11, 14, 10, 12)}, // Id set manually due to in-memory provider
            new TestRun { Id = 2, Build = "abcd124", TestTypeName = "API", Duration = 60.8, Link = "test.com", Created = new DateTime(2023, 9, 11, 18, 19, 42)},
            new TestRun { Id = 3, Build = "abcd125", TestTypeName = "API", Duration = 32.1, Link = "google.ca", Created = new DateTime(2023, 9, 12, 7, 49, 8)}
        );
        
        builder.Entity<TestCase>().ToTable("TestCases");
        builder.Entity<TestCase>().HasKey(p => p.Id);
        builder.Entity<TestCase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestCase>().Property(p => p.TestName).IsRequired().HasMaxLength(100);
        builder.Entity<TestCase>().Property(p => p.TestClass).IsRequired().HasMaxLength(100);
        builder.Entity<TestCase>().HasIndex(p => new {p.TestName, p.TestClass}).IsUnique();
        builder.Entity<TestCase>().HasMany(p => p.TestResults).WithOne(p => p.TestCase).HasForeignKey(p => p.TestCaseId);
        
        builder.Entity<TestCase>().HasData
        (
            new TestCase() { Id = 1, TestName = "Get Commerce Opportunities", TestClass = "E2E.Tests.RuntimeCommerceOpportunitiesTests"},
            new TestCase() { Id = 2, TestName = "ComOp Created In ComOp Service Is Retrieved From Runtime Service", TestClass = "E2E.Tests.EndToEndCommerceOpportunitiesTests"}

        );
        
        builder.Entity<TestResult>().ToTable("TestResults");
        builder.Entity<TestResult>().HasKey(p => p.Id);
        builder.Entity<TestResult>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestResult>().Property(p => p.Passed).IsRequired();
        builder.Entity<TestResult>().HasMany(p => p.TestResultBugs).WithOne(p => p.TestResult).HasForeignKey(p => p.TestResultId);
        builder.Entity<TestResult>().HasMany(p => p.TestMedia).WithOne(p => p.TestResult).HasForeignKey(p => p.TestResultId);
        builder.Entity<TestResult>().HasIndex(p => new {p.TestRunId, p.TestCaseId}).IsUnique();

        builder.Entity<TestResult>().HasData
        (
            new TestResult() { Id = 1, TestRunId = 1, TestCaseId = 1, Passed = true, Duration = 0.5}, // Id set manually due to in-memory provider
            new TestResult() { Id = 2, TestRunId = 1, TestCaseId = 2, Passed = true, Duration = 0.224},
            new TestResult() { Id = 3, TestRunId = 2, TestCaseId = 1, Passed = true, Duration = 0.931},
            new TestResult() { Id = 4, TestRunId = 2, TestCaseId = 2, Passed = true, Duration = 0.32},
            new TestResult() { Id = 5, TestRunId = 3, TestCaseId = 1, Passed = false, Duration = 0.48},
            new TestResult() { Id = 6, TestRunId = 3, TestCaseId = 2, Passed = true, Duration = 0.33333333333}
        );      
        
        builder.Entity<TestBug>().ToTable("TestBugs");
        builder.Entity<TestBug>().HasKey(p => p.Id);
        builder.Entity<TestBug>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestBug>().HasMany(p => p.TestResultBugs).WithOne(p => p.TestBug).HasForeignKey(p => p.TestBugId);
        
        builder.Entity<TestBug>().HasData
        (
            new TestBug() { Id = 1, Link = "https://google.ca", Identifier = "WEST-35"},
            new TestBug() { Id = 2, Link = "https://google2.ca", Identifier = "WEST-36"}
        );
        
        builder.Entity<TestResultBug>().ToTable("TestResultBugs");
        builder.Entity<TestResultBug>().HasKey(p => p.Id);
        builder.Entity<TestResultBug>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<TestResultBug>().HasData
        (
            new TestResultBug() { Id = 1, TestResultId = 5, TestBugId = 1}
        );

        builder.Entity<TestMedia>().ToTable("TestMedia");
        builder.Entity<TestMedia>().HasKey(p => p.Id);
        builder.Entity<TestMedia>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
    }
}
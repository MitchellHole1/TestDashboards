using Microsoft.EntityFrameworkCore;
using TestDashboard.Domain.Models;

namespace TestDashboard.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<TestRun> TestRuns { get; set; }
    public DbSet<TestCase> TestCases { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<TestBug> TestBugs { get; set; }
    public DbSet<TestMedia> TestMedia { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<TestRun>().ToTable("TestRuns");
        builder.Entity<TestRun>().HasKey(p => p.Id);
        builder.Entity<TestRun>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestRun>().Property(p => p.Build).IsRequired().HasMaxLength(15);
        builder.Entity<TestRun>().HasIndex(p => p.Build).IsUnique();
        builder.Entity<TestRun>().HasMany(p => p.TestResults).WithOne(p => p.TestRun).HasForeignKey(p => p.TestRunId);
        
        builder.Entity<TestRun>().HasData
        (
            new TestRun { Id = 1, Build = "abcd123", TestType = "API", Duration = 123, Link = "test.com"}, // Id set manually due to in-memory provider
            new TestRun { Id = 2, Build = "abcd124", TestType = "API", Duration = 125, Link = "test.com"},
            new TestRun { Id = 3, Build = "abcd125", TestType = "API", Duration = 125, Link = "google.ca"}

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
            new TestCase() { Id = 1, TestName = "GetAllItems", TestClass = "ItemsHappyPath"}, // Id set manually due to in-memory provider
            new TestCase() { Id = 2, TestName = "GetItem", TestClass = "ItemsHappyPath"},
            new TestCase() { Id = 3, TestName = "UpdateItem", TestClass = "ItemsHappyPath"},
            new TestCase() { Id = 4, TestName = "DeleteItem", TestClass = "ItemsHappyPath"}
        );
        
        builder.Entity<TestResult>().ToTable("TestResults");
        builder.Entity<TestResult>().HasKey(p => p.Id);
        builder.Entity<TestResult>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TestResult>().Property(p => p.Passed).IsRequired();
        builder.Entity<TestResult>().HasMany(p => p.TestBugs).WithOne(p => p.TestResult).HasForeignKey(p => p.TestResultId);
        builder.Entity<TestResult>().HasMany(p => p.TestMedia).WithOne(p => p.TestResult).HasForeignKey(p => p.TestResultId);
        builder.Entity<TestResult>().HasIndex(p => new {p.TestRunId, p.TestCaseId}).IsUnique();

        builder.Entity<TestResult>().HasData
        (
            new TestResult() { Id = 1, TestRunId = 1, TestCaseId = 1, Passed = false, Duration = 3}, // Id set manually due to in-memory provider
            new TestResult() { Id = 2, TestRunId = 1, TestCaseId = 2, Passed = true, Duration = 4},
            new TestResult() { Id = 3, TestRunId = 1, TestCaseId = 3, Passed = true, Duration = 4},
            new TestResult() { Id = 4, TestRunId = 1, TestCaseId = 4, Passed = false, Duration = 3},
            new TestResult() { Id = 5, TestRunId = 2, TestCaseId = 1, Passed = true, Duration = 4},
            new TestResult() { Id = 6, TestRunId = 2, TestCaseId = 2, Passed = true, Duration = 2},
            new TestResult() { Id = 7, TestRunId = 2, TestCaseId = 3, Passed = true, Duration = 4},
            new TestResult() { Id = 8, TestRunId = 2, TestCaseId = 4, Passed = true, Duration = 1},
            new TestResult() { Id = 9, TestRunId = 3, TestCaseId = 1, Passed = true, Duration = 1},
            new TestResult() { Id = 10, TestRunId = 3, TestCaseId = 2, Passed = true, Duration = 4},
            new TestResult() { Id = 11, TestRunId = 3, TestCaseId = 3, Passed = true, Duration = 6},
            new TestResult() { Id = 12, TestRunId = 3, TestCaseId = 4, Passed = true, Duration = 3}
        );      
        
        builder.Entity<TestBug>().ToTable("TestBugs");
        builder.Entity<TestBug>().HasKey(p => p.Id);
        builder.Entity<TestBug>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<TestBug>().HasData
        (
            new TestBug() { Id = 1, TestResultId = 4, Link = "google.ca"}
        );

        builder.Entity<TestMedia>().ToTable("TestMedia");
        builder.Entity<TestMedia>().HasKey(p => p.Id);
        builder.Entity<TestMedia>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
    }
}
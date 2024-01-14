namespace TestDashboard.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}
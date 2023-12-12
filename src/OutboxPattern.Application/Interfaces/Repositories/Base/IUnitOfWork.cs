namespace OutboxPattern.Application.Interfaces.Repositories.Base;
public interface IUnitOfWork
{
    Task SaveChangesAsync();
}

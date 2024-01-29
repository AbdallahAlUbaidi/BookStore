namespace Interfaces;

public interface IUnitOfWork
{
	public Task SaveChangesAsync();
}
namespace Data;

using System.Threading.Tasks;
using Interfaces;

public class UnitOfWork(DataContext context) : IUnitOfWork
{
	private readonly DataContext _Context = context;

    public async Task SaveChangesAsync()
	{
		await _Context.SaveChangesAsync();
	}
}
using Infrastructure.Persistences;
using MyTodo.Contracts;

namespace Infrastructure;

public class PersistenceFactory(string? path = null)
{
	public ITodoPersistence Create(string persistenceType)
	{
		switch (persistenceType)
		{
			case "json":
				return new JsonPersistence(path);
			case "sqlite":
				return new SqlitePersistence(path);
			default:
				throw new ArgumentException("Invalid persistence type");
		}
	}
}
using MyTodo.CLI.Persistences;
using MyTodo.Domain;

namespace MyTodo.CLI.Infra;

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
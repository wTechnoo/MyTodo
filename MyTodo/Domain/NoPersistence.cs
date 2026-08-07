using MyTodo.Contracts;

namespace MyTodo.Domain;

public class NoTodoPersistence : ITodoPersistence
{
	public void Save(IEnumerable<Todo> items)
	{
		
	}

	public IEnumerable<Todo> Load()
	{
		return [];
	}
}
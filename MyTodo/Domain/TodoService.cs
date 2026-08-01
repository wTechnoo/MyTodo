using System.Collections;

namespace MyTodo.Domain;

public class TodoService : IEnumerable<Todo>
{
	readonly ITodoPersistence _todoPersistence;
	List<Todo> _todos = [];

	public TodoService(ITodoPersistence? todoPersistence = null)
	{
		_todoPersistence = todoPersistence ?? new NoTodoPersistence();
	}

	public IEnumerator<Todo> GetEnumerator()
	{
		return _todos.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Load()
	{
		_todos = _todoPersistence.Load().ToList();
	}

	public void Save()
	{
		_todoPersistence.Save(_todos);
	}

	public void Add(Todo todo)
	{
		var nextId = _todos.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;
		todo = todo with { Id = nextId };
		_todos.Add(todo);
	}

	public void Remove(int id)
	{
		var todo = _todos.FirstOrDefault(x => x.Id == id);
		if (todo == null) throw new InvalidOperationException("Todo not found");
		_todos.Remove(todo);
	}
}

public interface ITodoPersistence
{
	void Save(IEnumerable<Todo> items);
	IEnumerable<Todo> Load();
}
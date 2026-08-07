using MyTodo.Domain;

namespace MyTodo.Contracts;

public interface ITodoPersistence
{
	void Save(IEnumerable<Todo> items);
	IEnumerable<Todo> Load();
}
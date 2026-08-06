namespace MyTodo.Domain;

public interface ITodoPersistence
{
	void Save(IEnumerable<Todo> items);
	IEnumerable<Todo> Load();
}
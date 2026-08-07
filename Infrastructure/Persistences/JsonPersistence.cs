using MyTodo.Contracts;
using MyTodo.Domain;
using Newtonsoft.Json;

namespace Infrastructure.Persistences;

public class JsonPersistence : ITodoPersistence
{
	static readonly string DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "Todos" + Path.DirectorySeparatorChar + "todos.json";
	readonly string _path;

	public JsonPersistence(string? path = null)
	{
		_path = path ?? DEFAULT_PATH;
		EnsureCreated();
	}

	public void Save(IEnumerable<Todo> items)
	{
		EnsureCreated();
		var json = JsonConvert.SerializeObject(items);
		File.WriteAllText(_path, json);
	}

	public IEnumerable<Todo> Load()
	{
		EnsureCreated();
		var json = File.ReadAllText(_path);
		return JsonConvert.DeserializeObject<List<Todo>>(json) ?? [];
	}

	void EnsureCreated()
	{
		if (!File.Exists(_path))
			File.Create(_path).Dispose();
	}
}
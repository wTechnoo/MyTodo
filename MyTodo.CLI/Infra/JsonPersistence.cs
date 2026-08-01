using MyTodo.Domain;
using Newtonsoft.Json;

namespace MyTodo.CLI.Infra;

public class JsonPersistence : ITodoPersistence
{
	const string PATH = "todos.json";

	public JsonPersistence()
	{
		EnsureCreated();
	}

	public void Save(IEnumerable<Todo> items)
	{
		EnsureCreated();
		var json = JsonConvert.SerializeObject(items);
		File.WriteAllText(PATH, json);
	}

	public IEnumerable<Todo> Load()
	{
		EnsureCreated();
		var json = File.ReadAllText(PATH);
		return JsonConvert.DeserializeObject<List<Todo>>(json) ?? [];
	}

	void EnsureCreated()
	{
		if (!File.Exists(PATH))
			File.Create(PATH).Dispose();
	}
}
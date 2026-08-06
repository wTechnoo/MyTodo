using System.Collections;
using Newtonsoft.Json.Linq;
using Tools.Config.Contracts;

namespace Tools.Config.Entities;

public class Config : IConfig
{
	static readonly string DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "Todos" + Path.DirectorySeparatorChar + "config.json";
	readonly JObject _content;
	readonly string _path;

	public Config() : this(null)
	{
	}

	public Config(string? path = null, IEnumerable<(string, string)> defaultValues = null)
	{
		_path = path ?? DEFAULT_PATH;
		_content = Load(_path, defaultValues);
		Save(_path, _content);
	}

	public string Get(string key, string fallback = null)
	{
		return _content.TryGetValue(key, out var token) ? token.ToString() : fallback;
	}

	public void Set(string key, string value)
	{
		_content[key] = value;

		if (!string.IsNullOrEmpty(_path))
			Save(_path, _content);
	}

	public IEnumerator<(string, string)> GetEnumerator()
	{
		foreach (var property in _content.Properties())
			yield return (property.Name, property.Value.ToString());
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	static JObject Load(string path, IEnumerable<(string, string)> defaults = null)
	{
		var content = new JObject();

		if (defaults != null)
		{
			foreach (var (key, value) in defaults)
				content[key] = value;
		}

		if (string.IsNullOrEmpty(path) || !File.Exists(path))
			return content;

		try
		{
			content.Merge(JObject.Parse(File.ReadAllText(path)));
		}
		catch
		{
		}

		return content;
	}

	static void Save(string path, JObject content)
	{
		var directory = Path.GetDirectoryName(path);

		if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
			Directory.CreateDirectory(directory);

		File.WriteAllText(path, content.ToString());
	}
}
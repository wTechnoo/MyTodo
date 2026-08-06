using System.Collections;

namespace MyTodo.CLI.Infra;

public class DefaultValues : IEnumerable<(string, string)>
{
	readonly List<(string, string)> _defaults =
	[
		("auto-persist", "false"),
		("persistence-type", "json")
	];

	public IEnumerator<(string, string)> GetEnumerator()
	{
		return _defaults.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
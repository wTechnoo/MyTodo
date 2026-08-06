using System.Collections;

namespace MyTodo.CLI.Infra;

public class DefaultValues : IEnumerable<(string, string)>
{
	readonly List<(string, string)> _defaults =
	[
		(Keys.AUTO_PERSIST, "false"),
		(Keys.PERSISTENCE_TYPE, "json")
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
using System.Collections;

namespace Infrastructure;

public class DefaultValues : IEnumerable<(string, string)>
{
	readonly List<(string, string)> _defaults =
	[
		(Keys.AUTO_PERSIST, "true"),
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
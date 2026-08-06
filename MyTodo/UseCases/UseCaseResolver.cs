using MyTodo.Domain;

namespace MyTodo.UseCases.Contracts;

public class UseCaseResolver
{
	readonly IErrorMessenger _errorMessenger;
	readonly IReadOnlyList<BaseUseCases> _groups;

	UseCaseResolver(IReadOnlyList<BaseUseCases> groups, IErrorMessenger errorMessenger)
	{
		_groups = groups;
		_errorMessenger = errorMessenger;
	}

	public static UseCaseResolver From(IErrorMessenger errorMessenger, params BaseUseCases[] groups)
	{
		return new UseCaseResolver(groups, errorMessenger);
	}

	public bool CanResolve(string input)
	{
		return _groups.Any(g => g.CanResolve(input));
	}

	public async Task Receive(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return;

		var group = _groups.FirstOrDefault(g => g.CanResolve(input));

		if (group == null)
		{
			_errorMessenger.SendError(Keys.UseCases.Errors.UNKNOWN_COMMAND);
			return;
		}

		await group.Receive(input);
	}

	public IReadOnlyList<string> Verbs()
	{
		return _groups.SelectMany(g => g.Verbs()).Distinct().ToArray();
	}
}
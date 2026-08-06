using MyTodo.CLI.Commands;
using MyTodo.Domain;
using MyTodo.UseCases;
using MyTodo.UseCases.Commands;
using MyTodo.UseCases.Contracts;
using Tools.Config.Contracts;

namespace MyTodo.CLI;

public class UseCases_CLI : BaseUseCases
{
	readonly IConfig _config;
	readonly IListUseCasesView _listUseCasesView;
	readonly UseCases_Todo _todoUseCases;

	public UseCases_CLI(IConfig config, UseCases_Todo todoUseCases, IListUseCasesView listUseCasesView, IErrorMessenger errorMessenger)
	{
		_config = config;
		_todoUseCases = todoUseCases;
		
		_listUseCasesView = listUseCasesView;
		_errorMessenger = errorMessenger;
	}

	[UseCase(Keys.Actions.LIST)]
	public ICommand ListAllUseCases()
	{
		var useCases = new List<string>();
		useCases.AddRange(_todoUseCases.Verbs());
		useCases.AddRange(Verbs());

		return new ListUseCasesCommand(_listUseCasesView, useCases);
	}

	[UseCase(Keys.Actions.EXIT)]
	public ICommand Exit()
	{
		return new ExitCommand(_todoUseCases, _config, _errorMessenger);
	}

	[UseCase(Keys.Actions.CHANGE_PERSISTENCE)]
	public ICommand ChangePersistence(string type)
	{
		return new ChangePersistenceCommand(_config, _errorMessenger, type);
	}

	[UseCase(Keys.Actions.CHANGE_AUTO_PERSIST)]
	public ICommand ChangeAutoPersist(bool value)
	{
		return new ChangeAutoPersistCommand(_config, _errorMessenger, value);
	}
}
using MyTodo.Domain;
using MyTodo.UseCases.Contracts;
using Tools.Config.Contracts;

namespace MyTodo.CLI.Commands;

public record ChangePersistenceCommand(IConfig Config, IErrorMessenger ErrorMessenger, string Type) : ICommand
{
	public void Execute()
	{
		if (string.IsNullOrWhiteSpace(Type) || !Keys.PersistenceTypes.Contains(Type))
		{
			ErrorMessenger.SendError(Keys.Errors.INVALID_PERSISTENCE_TYPE);
			return;
		}

		try
		{
			Config.Set(Keys.PERSISTENCE_TYPE, Type);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Errors.FAILED_TO_CHANGE_CONFIG, ex.Message);
		}
	}
}
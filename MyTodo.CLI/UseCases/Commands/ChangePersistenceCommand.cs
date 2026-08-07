using MyTodo.Contracts;
using Tools.Config.Contracts;

namespace MyTodo.CLI.UseCases.Commands;

public record ChangePersistenceCommand(IConfig Config, IErrorMessenger ErrorMessenger, string Type) : ICommand
{
	public void Execute()
	{
		if (string.IsNullOrWhiteSpace(Type) || !Infrastructure.Keys.PersistenceTypes.All.Contains(Type))
		{
			ErrorMessenger.SendError(Keys.Errors.INVALID_PERSISTENCE_TYPE);
			return;
		}

		try
		{
			Config.Set(Infrastructure.Keys.PERSISTENCE_TYPE, Type);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Errors.FAILED_TO_CHANGE_CONFIG, ex.Message);
		}
	}
}
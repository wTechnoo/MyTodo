using MyTodo.Domain;
using MyTodo.UseCases.Contracts;
using Tools.Config.Contracts;

namespace MyTodo.CLI.Commands;

public record ChangeAutoPersistCommand(IConfig Config, IErrorMessenger ErrorMessenger, bool Value) : ICommand
{
	public void Execute()
	{
		try
		{
			Config.Set(Keys.AUTO_PERSIST, Value.ToString());
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Errors.FAILED_TO_CHANGE_CONFIG, ex.Message);
		}
	}
}
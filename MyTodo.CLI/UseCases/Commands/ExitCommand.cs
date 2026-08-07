using MyTodo.Contracts;
using MyTodo.UseCases;
using Tools.Config.Contracts;

namespace MyTodo.CLI.UseCases.Commands;

public record ExitCommand(UseCases_Todo TodoUseCases, IConfig Config, IErrorMessenger ErrorMessenger) : ICommand
{
	public void Execute()
	{
		try
		{
			if (Config.Get("auto-persist") == "true")
				TodoUseCases.SaveTodos().Execute();
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Errors.FAILED_TO_EXIT, ex.Message);
		}

		Environment.Exit(0);
	}
}
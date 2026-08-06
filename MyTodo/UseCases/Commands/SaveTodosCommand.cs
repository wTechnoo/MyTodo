using MyTodo.Domain;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases.Commands;

public record SaveTodosCommand(TodoService TodoService, IErrorMessenger ErrorMessenger) : ICommand
{
	public void Execute()
	{
		try
		{
			TodoService.Save();
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.SAVE_FAILED, ex);
		}
	}
}

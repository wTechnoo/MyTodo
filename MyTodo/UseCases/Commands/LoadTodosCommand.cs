using MyTodo.Contracts;
using MyTodo.Domain;

namespace MyTodo.UseCases.Commands;

public record LoadTodosCommand(TodoService TodoService, IErrorMessenger ErrorMessenger) : ICommand
{
	public void Execute()
	{
		try
		{
			TodoService.Load();
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.LOAD_FAILED, ex);
		}
	}
}

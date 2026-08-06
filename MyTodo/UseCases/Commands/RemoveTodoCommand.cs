using MyTodo.Domain;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases.Commands;

public record RemoveTodoCommand(TodoService TodoService, IErrorMessenger ErrorMessenger, int Id) : ICommand
{
	public void Execute()
	{
		if (Id < 0)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.INVALID_ID);
			return;
		}

		try
		{
			TodoService.Remove(Id);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.REMOVE_FAILED, ex);
		}
	}
}
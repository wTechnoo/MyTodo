using MyTodo.Contracts;
using MyTodo.Domain;

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
		catch (KeyNotFoundException notFoundEx)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.REMOVE_TODO_NOT_FOUND, notFoundEx);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.REMOVE_FAILED, ex);
		}
	}
}
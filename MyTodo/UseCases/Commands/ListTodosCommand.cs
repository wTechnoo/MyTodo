using MyTodo.Contracts;
using MyTodo.Domain;

namespace MyTodo.UseCases.Commands;

public record ListTodosCommand(TodoService TodoService, IShowTodosView View, IErrorMessenger ErrorMessenger) : ICommand
{
	public void Execute()
	{
		if (!TodoService.Any())
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.NO_TODOS);
			return;
		}

		View.Todos = TodoService;
	}
}
using MyTodo.Domain;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases.Commands;

public record AddTodoCommand(TodoService TodoService, IErrorMessenger ErrorMessenger, string Title, string Description, bool Done = false) : ICommand
{
	public void Execute()
	{
		if (string.IsNullOrWhiteSpace(Title))
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.TITLE_EMPTY);
			return;
		}

		try
		{
			var todo = new Todo(0, Title, Description, Done);
			TodoService.Add(todo);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.ADD_FAILED, ex);
		}
	}
}
using MyTodo.Domain;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases.Commands;

public record UpdateTodoCommand(TodoService Todos, IErrorMessenger ErrorMessenger, int Id, string Title, string Description, bool? Done) : ICommand
{
	public void Execute()
	{
		if (Id < 0)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.INVALID_ID);
			return;
		}

		var todo = Todos.FirstOrDefault(x => x.Id == Id);
		if (todo is null)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.UPDATE_TODO_NOT_FOUND);
			return;
		}

		if (!string.IsNullOrWhiteSpace(Title))
			todo = todo with { Title = Title };

		if (!string.IsNullOrWhiteSpace(Description))
			todo = todo with { Description = Description };

		if (Done.HasValue)
			todo = todo with { Done = Done.Value };

		try
		{
			Todos.Update(todo);
		}
		catch (Exception ex)
		{
			ErrorMessenger.SendError(Keys.Todos.Errors.UPDATE_FAILED, ex.Message);
		}
	}
}
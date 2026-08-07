using MyTodo.Contracts;
using MyTodo.Domain;
using MyTodo.UseCases.Commands;

namespace MyTodo.CLI.Infra;

internal class ConsoleView : IListUseCasesView, IShowTodosView, IErrorMessenger
{
	public void SendError(string message, object? context = null)
	{
		Console.WriteLine("error: " + message);
	}

	public IEnumerable<string> UseCases
	{
		set => Console.WriteLine("usecases: " + string.Join(", ", value));
	}

	public IEnumerable<Todo> Todos
	{
		set
		{
			foreach (var todo in value)
				Console.WriteLine($"todo: id={todo.Id} title=[{todo.Title}] description=[{todo.Description}] done={todo.Done}");
		}
	}
}
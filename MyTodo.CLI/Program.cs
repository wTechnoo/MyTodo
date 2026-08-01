using MyTodo;
using MyTodo.CLI.Infra;
using MyTodo.Domain;
using MyTodo.UseCases;
using MyTodo.UseCases.Commands;

var view = new ConsoleView();
var jsonPersistence = new JsonPersistence();
var service = new TodoService(jsonPersistence);
var useCases = service.From(view, view, view);

await useCases.Receive(Keys.Todos.Actions.LOAD);

await useCases.Receive("list");

await useCases.Receive("add title=\"Buy milk\" description='2 liters, corner shop' done=true");
await useCases.Receive("add title=Walk");
await useCases.Receive("add title=\"Bad flag\" done=yse");
await useCases.Receive(Keys.Todos.Actions.LIST_TODOS);

await useCases.Receive(Keys.Todos.Actions.SAVE);

internal class ConsoleView : IListTodoUseCasesView, IShowTodosView, IErrorMessenger
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
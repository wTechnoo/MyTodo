using MyTodo.Contracts;
using MyTodo.Domain;

namespace MyTodo.ConsoleUI.Infra;

public class NoShowTodosView : IShowTodosView
{
	public IEnumerable<Todo> Todos
	{
		set { }
	}
}

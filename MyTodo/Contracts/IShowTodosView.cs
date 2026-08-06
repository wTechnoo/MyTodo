using MyTodo.Domain;

namespace MyTodo.UseCases.Commands;

public interface IShowTodosView
{
	IEnumerable<Todo> Todos { set; }
}
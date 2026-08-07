using MyTodo.Domain;

namespace MyTodo.Contracts;

public interface IShowTodosView
{
	IEnumerable<Todo> Todos { set; }
}
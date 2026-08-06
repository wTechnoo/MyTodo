using MyTodo.Domain;
using MyTodo.UseCases.Commands;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases;

public static partial class UseCases
{
	public static UseCases_Todo From(this TodoService todoService, IShowTodosView showTodosView, IErrorMessenger errorMessenger)
	{
		return new UseCases_Todo(todoService, showTodosView, errorMessenger);
	}
}
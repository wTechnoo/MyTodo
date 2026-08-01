using MyTodo.Domain;
using MyTodo.UseCases.Commands;

namespace MyTodo.UseCases;

public static class UseCases
{
	public static UseCases_Todo From(this TodoService todoService, IShowTodosView showTodosView, IListTodoUseCasesView listTodoUseCasesView, IErrorMessenger errorMessenger)
	{
		return new UseCases_Todo(todoService, listTodoUseCasesView, showTodosView, errorMessenger);
	}
}
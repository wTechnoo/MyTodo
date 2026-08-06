using MyTodo.Domain;
using MyTodo.UseCases.Commands;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases;

public class UseCases_Todo : BaseUseCases
{
	readonly IShowTodosView _showTodosView;
	readonly TodoService _todoService;

	public UseCases_Todo(TodoService todoService, IShowTodosView showTodosView, IErrorMessenger errorMessenger)
	{
		_todoService = todoService;
		_showTodosView = showTodosView;
		_errorMessenger = errorMessenger;
	}

	[UseCase(Keys.Todos.Actions.ADD)]
	public ICommand AddTodo(string title, string description, bool done = false)
	{
		return new AddTodoCommand(_todoService, _errorMessenger, title, description, done);
	}
	
	[UseCase(Keys.Todos.Actions.UPDATE)]
	public ICommand UpdateTodo(int id, string title, string description, bool? done)
	{
		return new UpdateTodoCommand(_todoService, _errorMessenger, id, title, description, done);
	}

	[UseCase(Keys.Todos.Actions.REMOVE)]
	public ICommand RemoveTodo(int id)
	{
		return new RemoveTodoCommand(_todoService, _errorMessenger, id);
	}

	[UseCase(Keys.Todos.Actions.LIST_TODOS)]
	public ICommand ListTodos()
	{
		return new ListTodosCommand(_todoService, _showTodosView, _errorMessenger);
	}

	[UseCase(Keys.Todos.Actions.SAVE)]
	public ICommand SaveTodos()
	{
		return new SaveTodosCommand(_todoService, _errorMessenger);
	}

	[UseCase(Keys.Todos.Actions.LOAD)]
	public ICommand LoadTodos()
	{
		return new LoadTodosCommand(_todoService, _errorMessenger);
	}
}
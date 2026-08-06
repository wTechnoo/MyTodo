using MyTodo.Domain;
using MyTodo.UseCases.Commands;
using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases;

public class UseCases_Todo : BaseUseCases
{
	readonly IListUseCasesView _listUseCasesView;
	readonly IShowTodosView _showTodosView;
	readonly TodoService _todoService;

	public UseCases_Todo(TodoService todoService, IListUseCasesView listUseCasesView, IShowTodosView showTodosView, IErrorMessenger errorMessenger)
	{
		_todoService = todoService;
		_listUseCasesView = listUseCasesView;
		_showTodosView = showTodosView;
		_errorMessenger = errorMessenger;
	}

	[UseCase(Keys.Todos.Actions.ADD)]
	public ICommand AddTodo(string title, string description, bool done = false)
	{
		return new AddTodoCommand(_todoService, _errorMessenger, title, description, done);
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
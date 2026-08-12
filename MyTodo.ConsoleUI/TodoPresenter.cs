using MyTodo.ConsoleUI.ViewModels;
using MyTodo.Domain;
using MyTodo.UseCases;
using static MyTodo.Keys.Todos;

namespace MyTodo.ConsoleUI;

public class TodoPresenter : ITodoPresenter
{
	readonly bool _autoPersist;
	readonly UseCaseResolver _context;
	readonly TodoService _todoService;
	readonly ITodoView _view;
	ViewModel _viewModel;

	public TodoPresenter(ITodoView view, UseCaseResolver context, TodoService todoService, bool autoPersist = false)
	{
		_view = view;
		_context = context;
		_todoService = todoService;
		_autoPersist = autoPersist;

		_viewModel = new ViewModel(_todoService);
		_view.ViewModel = _viewModel;

		view.Set(this);
	}

	public Task AddTodo(string title, string description)
	{
		_viewModel.Add(new VM_Todo(title, description));

		return Submit();
	}

	public Task RemoveTodo(int id)
	{
		var todo = _viewModel.OfType<VM_Todo>().FirstOrDefault(vm => vm.Id == id);

		if (todo == null)
			return Task.CompletedTask;

		_viewModel.Remove(todo);

		return Submit();
	}

	public Task ChangeDone(bool value, int id)
	{
		var todo = _viewModel.OfType<VM_Todo>().FirstOrDefault(vm => vm.Id == id);

		if (todo == null)
			return Task.CompletedTask;

		todo.Done = value;

		return Submit();
	}

	public async Task Reload()
	{
		await _context.Receive(Actions.LOAD);

		Rebuild();
	}

	public async Task Submit()
	{
		var commands = _viewModel.ChangeSet.ToArray();

		foreach (var command in commands)
			await _context.Receive(command);

		if (_autoPersist && commands.Length > 0)
			await _context.Receive(Actions.SAVE);

		Rebuild();
	}

	void Rebuild()
	{
		_viewModel = new ViewModel(_todoService);
		_view.ViewModel = _viewModel;
	}
}

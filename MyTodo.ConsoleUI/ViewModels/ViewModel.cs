using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using MyTodo.Domain;
using static MyTodo.Keys.Todos;

namespace MyTodo.ConsoleUI.ViewModels;

public sealed class ViewModel : ObservableCollection<VM_Base>
{
	readonly List<string> _changes = [];

	public ViewModel(IEnumerable<Todo> todos)
	{
		foreach (var todo in todos)
		{
			var vm = new VM_Todo(todo.Id, todo.Title, todo.Description, todo.Done);

			vm.PropertyChanged += OnTodoChanged;

			Add(vm);
		}

		CollectionChanged += OnCurrentVMsChanged;
	}

	public IEnumerable<string> ChangeSet => _changes;

	void OnCurrentVMsChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		if (e.NewItems != null)
			foreach (var todo in e.NewItems.OfType<VM_Todo>())
			{
				todo.PropertyChanged += OnTodoChanged;

				_changes.Add($"{Actions.ADD} {Arguments.TITLE}={Quoted(todo.Title)} {Arguments.DESCRIPTION}={Quoted(todo.Description)} {Arguments.DONE}={Flag(todo.Done)}");
			}

		if (e.OldItems != null)
			foreach (var todo in e.OldItems.OfType<VM_Todo>())
			{
				todo.PropertyChanged -= OnTodoChanged;

				_changes.Add($"{Actions.REMOVE} {Arguments.ID}={todo.Id}");
			}
	}

	void OnTodoChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (sender is not VM_Todo todo || todo.Id == 0)
			return;

		if (e.PropertyName == nameof(VM_Todo.Done))
			_changes.Add($"{Actions.UPDATE} {Arguments.ID}={todo.Id} {Arguments.DONE}={Flag(todo.Done)}");
	}

	static string Quoted(string value)
	{
		if (!value.Contains('\''))
			return $"'{value}'";

		if (!value.Contains('"'))
			return $"\"{value}\"";

		return $"'{value.Replace("'", string.Empty)}'";
	}

	static string Flag(bool value)
	{
		return value ? "true" : "false";
	}
}

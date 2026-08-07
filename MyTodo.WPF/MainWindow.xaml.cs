using System.Collections.ObjectModel;
using System.Windows;
using MyTodo.Contracts;
using MyTodo.Domain;
using MyTodo.UseCases;
using Tools.Config.Contracts;

namespace MyTodo.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IShowTodosView, IErrorMessenger
{
	readonly UseCaseResolver _useCases;

	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;
		Closed += MainWindow_Closed;
		
		var config = App.Services.Get<IConfig>();
		var todoService = App.Services.Get<TodoService>();

		var todoUseCases = todoService.From(this, this);
		_useCases = UseCaseResolver.From(this, todoUseCases);

		if (config.Get(Infrastructure.Keys.AUTO_PERSIST) == "true")
			todoUseCases.LoadTodos().Execute();
		
		todoUseCases.ListTodos().Execute();
	}

	async void MainWindow_Closed(object? sender, EventArgs e)
	{
		await _useCases.Receive(Keys.Todos.Actions.SAVE);
	}

	public void SendError(string message, object? context = null)
	{
		MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}

	public ObservableCollection<TodoVm> TodosViewModel { get; } = new();

	public IEnumerable<Todo> Todos
	{
		set => UpdateViewModels(value);
	}

	void UpdateViewModels(IEnumerable<Todo> todos)
	{
		TodosViewModel.Clear();
		foreach(var todo in todos)
			TodosViewModel.Add(todo.ToVm());
	}
}

public record TodoVm(int Id, string Title, string Description, bool Done);

public static class TodoVmExtensions
{
	public static TodoVm ToVm(this Todo todo) => new TodoVm(todo.Id, todo.Title, todo.Description, todo.Done);
}
using ConsoleButtons;
using MyTodo.ConsoleUI.ViewModels;

namespace MyTodo.ConsoleUI.Infra;

public class ConsoleButtonsTodoView : ITodoView
{
	const int HeaderRow = 0;
	const int ToolbarRow = 2;
	const int ListRow = 4;
	const int TitleWidth = 26;
	const int DescriptionWidth = 30;
	const int FrameDelay = 16;

	readonly List<UIComponent> _components = [];
	readonly Queue<Func<Task>> _intents = new();
	readonly UIManager _manager = new();

	string? _error;
	int _promptRow = ListRow + 1;
	ITodoPresenter _presenter = null!;
	ICollection<VM_Base> _viewModel = [];

	public bool IsRunning { get; private set; } = true;

	public ICollection<VM_Base> ViewModel
	{
		set
		{
			_viewModel = value;
			Render();
		}
	}

	public void Set(ITodoPresenter todoPresenter)
	{
		_presenter = todoPresenter;
	}

	public void SendError(string message, object? context = null)
	{
		_error = message;

		WriteError(message);
	}

	public async Task Tick()
	{
		_manager.Update();

		while (_intents.Count > 0)
		{
			_error = null;

			await _intents.Dequeue()();
		}

		await Task.Delay(FrameDelay);
	}

	void Render()
	{
		foreach (var component in _components)
			_manager.RemoveComponent(component);

		_components.Clear();
		Console.Clear();

		WriteAt(0, HeaderRow, "MY TODOS", ConsoleColor.White);

		AddButton("[ + Add ]", 0, ToolbarRow, () => _intents.Enqueue(PromptAndAdd));
		AddButton("[ Reload ]", 11, ToolbarRow, () => _intents.Enqueue(_presenter.Reload));
		AddButton("[ Quit ]", 23, ToolbarRow, Stop);

		var row = ListRow;

		foreach (var todo in _viewModel.OfType<VM_Todo>())
		{
			var label = RowLabel(todo);
			var id = todo.Id;
			
			AddCheckbox(todo.Done, 0, row, () => _intents.Enqueue(() => _presenter.ChangeDone(!todo.Done, id)));

			WriteAt(0, row, label, ConsoleColor.Gray);
			AddButton("[ remove ]", label.Length + 2, row, () => _intents.Enqueue(() => _presenter.RemoveTodo(id)));
			
			row++;
		}

		if (row == ListRow)
			WriteAt(0, ListRow, "nothing here yet", ConsoleColor.DarkGray);

		_promptRow = row + 1;

		if (_error != null)
			WriteError(_error);
	}

	void AddSlider(int value, int column, int row, Action onClick)
	{
		var slider = new Slider(value, 10, 10, true, '█', ' ', column, row);
		
		slider.OnHoverOver += () => slider.WriteWithColor(ConsoleColor.Cyan);
		slider.OnHoverStop += slider.WriteWithNoColor;
		slider.OnClick += () => slider.WriteWithColor(ConsoleColor.Green);
		slider.OnHold += () => { slider.WriteWithColor(ConsoleColor.Gray); Console.Write(slider.Value); };
		slider.OnClick += onClick;
		
		_components.Add(slider);
		_manager.AddToComponents(slider);
	}

	void AddCheckbox(bool done, int column, int row, Action onClick)
	{
		var checkBox = new CheckBox(string.Empty, 'X', done, column, row);
		
		checkBox.OnHoverOver += () => checkBox.WriteWithColor(ConsoleColor.Cyan);
		checkBox.OnHoverStop += checkBox.WriteWithNoColor;
		checkBox.OnClick += () => checkBox.WriteWithColor(ConsoleColor.Green);
		checkBox.OnClick += onClick;
		
		_components.Add(checkBox);
		_manager.AddToComponents(checkBox);
	}

	void AddButton(string text, int column, int row, Action onClick)
	{
		var button = new Button(text, column, row);

		button.OnHoverOver += () => button.WriteWithColor(ConsoleColor.Cyan);
		button.OnHoverStop += button.WriteWithNoColor;
		button.OnClick += () => button.WriteWithColor(ConsoleColor.Green);
		button.OnClick += onClick;

		_components.Add(button);
		_manager.AddToComponents(button);
	}

	async Task PromptAndAdd()
	{
		var title = Prompt("title:", _promptRow);
		var description = Prompt("description:", _promptRow + 1);

		await _presenter.AddTodo(title, description);
	}

	string Prompt(string label, int row)
	{
		ClearRow(row);
		WriteAt(0, row, label, ConsoleColor.Yellow);

		Console.ForegroundColor = ConsoleColor.White;
		Console.SetCursorPosition(label.Length + 1, row);
		Console.CursorVisible = true;

		var value = Console.ReadLine() ?? string.Empty;

		Console.CursorVisible = false;

		return value.Trim();
	}

	void Stop()
	{
		IsRunning = false;
	}

	void WriteError(string message)
	{
		WriteAt(0, _promptRow, "! " + message, ConsoleColor.Red);
	}

	static string RowLabel(VM_Todo todo)
	{
		return $"  {todo.Id,4}  {Fit(todo.Title, TitleWidth)}  {Fit(todo.Description, DescriptionWidth)}";
	}

	static string Fit(string value, int width)
	{
		var flattened = (value ?? string.Empty).Replace('\r', ' ').Replace('\n', ' ');

		return flattened.Length <= width ? flattened.PadRight(width) : flattened[..(width - 1)] + "…";
	}

	static void ClearRow(int row)
	{
		WriteAt(0, row, new string(' ', Math.Max(0, Console.BufferWidth - 1)), ConsoleColor.White);
	}

	static void WriteAt(int column, int row, string text, ConsoleColor color)
	{
		if (Console.IsOutputRedirected)
			return;

		if (column < 0 || row < 0 || column >= Console.BufferWidth || row >= Console.BufferHeight)
			return;

		Console.ForegroundColor = color;
		Console.SetCursorPosition(column, row);
		Console.Write(text);
	}
}

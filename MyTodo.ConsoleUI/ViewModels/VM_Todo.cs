namespace MyTodo.ConsoleUI.ViewModels;

public class VM_Todo : VM_Base
{
	bool _done;

	public VM_Todo(int id, string title, string description, bool done)
	{
		Id = id;
		Title = title;
		Description = description;
		_done = done;
	}

	public VM_Todo(string title, string description) : this(0, title, description, false)
	{
	}

	public int Id { get; }
	public string Title { get; }
	public string Description { get; }

	public bool Done
	{
		get => _done;
		set => SetField(ref _done, value);
	}
}

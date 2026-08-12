namespace MyTodo.ConsoleUI;

public interface ITodoPresenter
{
	Task AddTodo(string title, string description);
	Task RemoveTodo(int id);
	Task ChangeDone(bool value, int id);
	Task Reload();
	Task Submit();
}

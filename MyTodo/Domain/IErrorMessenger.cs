namespace MyTodo.UseCases;

public interface IErrorMessenger
{
	void SendError(string message, object? context = null);
}
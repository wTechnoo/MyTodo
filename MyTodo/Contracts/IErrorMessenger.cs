namespace MyTodo.Contracts;

public interface IErrorMessenger
{
	void SendError(string message, object? context = null);
}
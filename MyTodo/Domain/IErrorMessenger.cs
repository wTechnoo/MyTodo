namespace MyTodo.Domain;

public interface IErrorMessenger
{
	void SendError(string message, object? context = null);
}
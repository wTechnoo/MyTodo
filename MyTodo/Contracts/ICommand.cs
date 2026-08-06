namespace MyTodo.UseCases.Contracts;

public interface ICommand
{
	void Execute();
}

public interface IAsyncCommand
{
	Task ExecuteAsync();
}
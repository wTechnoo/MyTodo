namespace MyTodo.UseCases;

public interface ICommand
{
	void Execute();
}

public interface IAsyncCommand
{
	Task ExecuteAsync();
}
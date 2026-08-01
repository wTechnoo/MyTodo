namespace MyTodo.UseCases;

public record ListUseCasesCommand(IListTodoUseCasesView View, IReadOnlyList<string> UseCases) : ICommand
{
	public void Execute()
	{
		View.UseCases = UseCases;
	}
}
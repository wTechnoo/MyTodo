using MyTodo.UseCases.Contracts;

namespace MyTodo.UseCases.Commands;

public record ListUseCasesCommand(IListUseCasesView View, IReadOnlyList<string> UseCases) : ICommand
{
	public void Execute()
	{
		View.UseCases = UseCases;
	}
}
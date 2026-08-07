using MyTodo.Contracts;

namespace MyTodo.CLI.UseCases.Commands;

public record ListUseCasesCommand(IListUseCasesView View, IReadOnlyList<string> UseCases) : ICommand
{
	public void Execute()
	{
		View.UseCases = UseCases;
	}
}
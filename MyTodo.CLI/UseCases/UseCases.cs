using MyTodo.Contracts;
using MyTodo.UseCases;
using Tools.Config.Contracts;

namespace MyTodo.CLI.UseCases;

public static partial class UseCases
{
	public static UseCases_CLI From(this IConfig config, UseCases_Todo todoUseCases, IListUseCasesView listUseCasesView, IErrorMessenger errorMessenger)
	{
		return new UseCases_CLI(config, todoUseCases, listUseCasesView, errorMessenger);
	}
}
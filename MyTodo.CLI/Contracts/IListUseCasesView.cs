namespace MyTodo.UseCases.Contracts;

public interface IListUseCasesView
{
	IEnumerable<string> UseCases { set; }
}
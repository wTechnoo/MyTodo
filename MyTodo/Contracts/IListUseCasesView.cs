namespace MyTodo.Contracts;

public interface IListUseCasesView
{
	IEnumerable<string> UseCases { set; }
}
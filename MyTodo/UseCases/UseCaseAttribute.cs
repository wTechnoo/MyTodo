namespace MyTodo.UseCases.Contracts;

[AttributeUsage(AttributeTargets.Method)]
public sealed class UseCaseAttribute : Attribute
{
	public UseCaseAttribute(string action) => Action = action;

	public string Action { get; }
}
namespace MyTodo.Domain;

public record Todo(int Id, string Title, string Description, bool Done)
{
}
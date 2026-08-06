using MyTodo.CLI;
using MyTodo.CLI.Infra;
using MyTodo.Domain;
using MyTodo.UseCases;
using MyTodo.UseCases.Contracts;
using Tools.Config.Entities;

var config = new Config(defaultValues: new DefaultValues());
var persistenceType = config.Get(Keys.PERSISTENCE_TYPE, "json");

var view = new ConsoleView();
var persistence = new PersistenceFactory().Create(persistenceType);
var service = new TodoService(persistence);

var todoUseCases = service.From(view, view);
var cliUseCases = config.From(todoUseCases, view, view);

var useCases = UseCaseResolver.From(view, todoUseCases, cliUseCases);

await useCases.Receive("list");
if (config.Get(Keys.AUTO_PERSIST) == "true")
	todoUseCases.LoadTodos().Execute();

while (true)
{
	var input = Console.ReadLine();
	if (input is null)
		return;
	
	await useCases.Receive(input);
}
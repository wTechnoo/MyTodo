using Infrastructure;
using MyTodo.CLI;
using MyTodo.CLI.Infra;
using MyTodo.CLI.UseCases;
using MyTodo.Domain;
using MyTodo.UseCases;
using Tools.Config.Entities;
using Keys = Infrastructure.Keys;

var config = new Config(defaultValues: new DefaultValues());
var persistenceType = config.Get(Keys.PERSISTENCE_TYPE, "json");

var view = new ConsoleView();
var persistence = new PersistenceFactory().Create(persistenceType);
var service = new TodoService(persistence);

var todoUseCases = service.From(view, view);
var cliUseCases = config.From(todoUseCases, view, view);

var useCases = UseCaseResolver.From(view, todoUseCases, cliUseCases);

if (config.Get(Keys.AUTO_PERSIST) == "true")
	todoUseCases.LoadTodos().Execute();

await useCases.Receive("list");

while (true)
{
	var input = Console.ReadLine();

	if (input is null)
		return;

	await useCases.Receive(input);
}
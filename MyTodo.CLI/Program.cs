using MyTodo.CLI;
using MyTodo.CLI.Infra;
using MyTodo.Domain;
using MyTodo.UseCases;
using MyTodo.UseCases.Contracts;
using Tools.Config.Entities;

var config = new Config(defaultValues: new DefaultValues());
var persistenceType = config.Get("persistence-type", "json");

var view = new ConsoleView();
var persistence = new PersistenceFactory().Create(persistenceType);
var service = new TodoService(persistence);

var todoUseCases = service.From(view, view, view);
var cliUseCases = config.From(todoUseCases, view, view);

var useCases = UseCaseResolver.From(view, todoUseCases, cliUseCases);

await useCases.Receive("list");

while (true)
{
	var input = Console.ReadLine();

	await useCases.Receive(input);
}
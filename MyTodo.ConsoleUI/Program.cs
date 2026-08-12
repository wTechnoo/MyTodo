using Infrastructure;
using MyTodo.ConsoleUI;
using MyTodo.ConsoleUI.Infra;
using MyTodo.Domain;
using MyTodo.UseCases;
using Tools.Config.Entities;
using Keys = Infrastructure.Keys;

Console.Title = "My Todos";

var config = new Config(defaultValues: new DefaultValues());
var persistenceType = config.Get(Keys.PERSISTENCE_TYPE, Keys.PersistenceTypes.JSON);
var autoPersist = config.Get(Keys.AUTO_PERSIST).ToLower() == "true";

var view = new ConsoleButtonsTodoView();
var persistence = new PersistenceFactory().Create(persistenceType);
var service = new TodoService(persistence);

var todoUseCases = service.From(new NoShowTodosView(), view);
var useCases = UseCaseResolver.From(view, todoUseCases);

if (autoPersist)
	todoUseCases.LoadTodos().Execute();

_ = new TodoPresenter(view, useCases, service, autoPersist);

while (view.IsRunning)
	await view.Tick();

Console.ResetColor();
Console.CursorVisible = true;
Console.Clear();
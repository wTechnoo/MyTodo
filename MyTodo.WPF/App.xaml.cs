using System.Windows;
using Infrastructure;
using MyTodo.Domain;
using MyTodo.UseCases;
using Tools.Config.Entities;
using Tools.Services;

namespace MyTodo.WPF;

public partial class App : Application
{
	public static IServices Services { get; } = new Services();
	
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var config = new Config(defaultValues: new DefaultValues());
		var persistenceType = config.Get(Infrastructure.Keys.PERSISTENCE_TYPE, "json");
		
		var persistence = new PersistenceFactory().Create(persistenceType);
		var service = new TodoService(persistence);

		Services.Register(config);
		Services.Register(persistence);
		Services.Register(service);
	}
}
namespace Tools.Services;

public class Services : IServices
{
	List<object> _services;

	public Services()
	{
		_services = new List<object>();
	}

	public void Register(object service)
	{
		_services.Add(service);
	}

	public T Get<T>()
	{
		return _services.OfType<T>().FirstOrDefault();
	}
}
namespace Tools.Services;

public interface IServices
{
	T Get<T>();
	void Register(object service);
}
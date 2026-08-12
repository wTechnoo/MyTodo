using MyTodo.ConsoleUI.ViewModels;
using MyTodo.Contracts;

namespace MyTodo.ConsoleUI;

public interface ITodoView : IErrorMessenger
{
	ICollection<VM_Base> ViewModel { set; }
	void Set(ITodoPresenter todoPresenter);
}

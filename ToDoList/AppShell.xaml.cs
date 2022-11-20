using ToDoList.Pages;

namespace ToDoList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ManageTodoPage), typeof(ManageTodoPage));
	}
}

using MauiTodo.Views;

namespace MauiTodo;

public partial class AppShell
{
	public AppShell(MainPage mainPage)
	{
		InitializeComponent();

		Items.Add(new ShellContent
		{
			Title = "Tasks",
			Content = mainPage,
			Route = nameof(MainPage)
		});
	}
}

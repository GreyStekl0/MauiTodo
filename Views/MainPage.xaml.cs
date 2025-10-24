using MauiTodo.ViewModels;

namespace MauiTodo.Views;

public partial class MainPage
{
	public MainPage(TodoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

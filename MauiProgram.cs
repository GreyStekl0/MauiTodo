using MauiTodo.Data;
using MauiTodo.Services;
using MauiTodo.ViewModels;
using MauiTodo.Views;
using Microsoft.Extensions.Logging;

namespace MauiTodo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<TodoDatabase>();
		builder.Services.AddSingleton<ITodoRepository, SqliteTodoRepository>();
		builder.Services.AddSingleton<TodoViewModel>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<AppShell>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

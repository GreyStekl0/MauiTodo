using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTodo.Models;

public partial class TodoItem : ObservableObject
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private bool _isCompleted;
}

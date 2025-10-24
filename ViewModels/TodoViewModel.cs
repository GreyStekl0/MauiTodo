using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodo.Models;
using MauiTodo.Services;

namespace MauiTodo.ViewModels;

public partial class TodoViewModel : ObservableObject
{
    private readonly ITodoRepository _repository;

    [ObservableProperty] private string _newTitle = string.Empty;

    public ObservableCollection<TodoItem> Items { get; }

    public IRelayCommand AddTodoCommand { get; }

    public IRelayCommand<TodoItem> ToggleCompletedCommand { get; }

    public IRelayCommand<TodoItem> DeleteTodoCommand { get; }

    public TodoViewModel(ITodoRepository repository)
    {
        _repository = repository;
        Items = repository.Items;

        AddTodoCommand = new RelayCommand(AddTodo, CanAddTodo);
        ToggleCompletedCommand = new RelayCommand<TodoItem>(ToggleCompleted);
        DeleteTodoCommand = new RelayCommand<TodoItem>(DeleteTodo);
    }

    partial void OnNewTitleChanged(string value)
    {
        _ = value;
        AddTodoCommand.NotifyCanExecuteChanged();
    }

    private bool CanAddTodo() => !string.IsNullOrWhiteSpace(NewTitle);

    private void AddTodo()
    {
        if (!CanAddTodo())
        {
            return;
        }

        var item = new TodoItem
        {
            Title = NewTitle.Trim()
        };

        _repository.Add(item);
        NewTitle = string.Empty;
    }

    private void ToggleCompleted(TodoItem? item)
    {
        if (item is null)
        {
            return;
        }

        item.IsCompleted = !item.IsCompleted;
        _repository.Update(item);
    }

    private void DeleteTodo(TodoItem? item)
    {
        if (item is null)
        {
            return;
        }

        _repository.Remove(item);
    }
}

using System.Collections.ObjectModel;
using MauiTodo.Models;

namespace MauiTodo.Services;

public interface ITodoRepository
{
    ObservableCollection<TodoItem> Items { get; }

    TodoItem Add(TodoItem item);

    void Update(TodoItem item);

    void Remove(TodoItem item);
}

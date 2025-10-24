using System.Collections.ObjectModel;
using MauiTodo.Models;

namespace MauiTodo.Services;

public class InMemoryTodoRepository : ITodoRepository
{
    public ObservableCollection<TodoItem> Items { get; } = [];

    public TodoItem Add(TodoItem item)
    {
        Items.Add(item);
        return item;
    }

    public void Update(TodoItem item)
    {
        var existing = Items.FirstOrDefault(x => x.Id == item.Id);
        if (existing is null)
        {
            return;
        }

        existing.Title = item.Title;
        existing.IsCompleted = item.IsCompleted;
    }

    public void Remove(TodoItem item)
    {
        var existing = Items.FirstOrDefault(x => x.Id == item.Id);
        if (existing is null)
        {
            return;
        }

        Items.Remove(existing);
    }
}

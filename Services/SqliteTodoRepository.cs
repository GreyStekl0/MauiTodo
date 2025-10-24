using System.Collections.ObjectModel;
using MauiTodo.Data;
using MauiTodo.Models;

namespace MauiTodo.Services;

public class SqliteTodoRepository : ITodoRepository
{
    private readonly TodoDatabase _database;

    public ObservableCollection<TodoItem> Items { get; }

    public SqliteTodoRepository(TodoDatabase database)
    {
        _database = database;
        Items = new ObservableCollection<TodoItem>();
        LoadItems();
    }

    private void LoadItems()
    {
        var items = _database.GetItems()
            .Select(ToModel);

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public TodoItem Add(TodoItem item)
    {
        var entity = ToEntity(item);
        _database.InsertItem(entity);
        Items.Add(item);
        return item;
    }

    public void Update(TodoItem item)
    {
        var entity = ToEntity(item);
        _database.UpdateItem(entity);
    }

    public void Remove(TodoItem item)
    {
        _database.DeleteItem(item.Id);

        var existing = Items.FirstOrDefault(x => x.Id == item.Id);
        if (existing is null)
        {
            return;
        }

        Items.Remove(existing);
    }

    private static TodoItem ToModel(TodoDatabase.TodoItemEntity entity) =>
        new()
        {
            Id = entity.Id,
            Title = entity.Title,
            IsCompleted = entity.IsCompleted
        };

    private static TodoDatabase.TodoItemEntity ToEntity(TodoItem item) =>
        new()
        {
            Id = item.Id,
            Title = item.Title,
            IsCompleted = item.IsCompleted
        };
}

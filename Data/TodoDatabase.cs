using SQLite;

namespace MauiTodo.Data;

public sealed class TodoDatabase : IDisposable
{
    private const string DatabaseFilename = "mauitodo.db3";
    private readonly SQLiteConnection _connection;
    private bool _initialized;

    public TodoDatabase()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex;
        _connection = new SQLiteConnection(databasePath, flags);
        Initialize();
    }

    private void Initialize()
    {
        _connection.CreateTable<TodoItemEntity>();
        _initialized = true;
    }

    public IReadOnlyList<TodoItemEntity> GetItems() =>
        _connection.Table<TodoItemEntity>().ToList();

    public void InsertItem(TodoItemEntity entity)
    {
        _connection.Insert(entity);
    }

    public void UpdateItem(TodoItemEntity entity)
    {
        _connection.Update(entity);
    }

    public void DeleteItem(Guid id)
    {
        _connection.Delete<TodoItemEntity>(id);
    }

    public void Dispose()
    {
        _connection.Close();
    }

    [Table("TodoItems")]
    public class TodoItemEntity
    {
        [PrimaryKey] public Guid Id { get; set; }

        [NotNull] public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}

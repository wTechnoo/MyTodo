using Microsoft.Data.Sqlite;
using MyTodo.Domain;

namespace MyTodo.CLI.Persistences;

public class SqlitePersistence : ITodoPersistence
{
	static readonly string DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "Todos" + Path.DirectorySeparatorChar + "todos.sqlite";
	readonly string _connectionString;

	public SqlitePersistence(string? path = null)
	{
		var fullPath = Path.GetFullPath(path ?? DEFAULT_PATH);
		Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
		_connectionString = new SqliteConnectionStringBuilder { DataSource = fullPath }.ToString();

		EnsureCreated();
	}

	public void Save(IEnumerable<Todo> items)
	{
		using var connection = Open();
		using var transaction = connection.BeginTransaction();

		using (var clear = connection.CreateCommand())
		{
			clear.Transaction = transaction;
			clear.CommandText = "DELETE FROM Todos";
			clear.ExecuteNonQuery();
		}

		using var insert = connection.CreateCommand();
		insert.Transaction = transaction;
		insert.CommandText = "INSERT INTO Todos(Id, Title, Description, Done) VALUES($id, $title, $description, $done)";

		var id = insert.Parameters.Add("$id", SqliteType.Integer);
		var title = insert.Parameters.Add("$title", SqliteType.Text);
		var description = insert.Parameters.Add("$description", SqliteType.Text);
		var done = insert.Parameters.Add("$done", SqliteType.Integer);

		foreach (var item in items)
		{
			id.Value = item.Id;
			title.Value = item.Title;
			description.Value = item.Description;
			done.Value = item.Done;
			insert.ExecuteNonQuery();
		}

		transaction.Commit();
	}

	public IEnumerable<Todo> Load()
	{
		using var connection = Open();
		using var cmd = connection.CreateCommand();
		cmd.CommandText = "SELECT Id, Title, Description, Done FROM Todos ORDER BY Id";

		var todos = new List<Todo>();
		using var reader = cmd.ExecuteReader();

		while (reader.Read())
			todos.Add(new Todo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

		return todos;
	}

	void EnsureCreated()
	{
		using var connection = Open();
		using var cmd = connection.CreateCommand();
		cmd.CommandText = "CREATE TABLE IF NOT EXISTS Todos(Id INTEGER PRIMARY KEY, Title TEXT NOT NULL, Description TEXT NOT NULL, Done INTEGER NOT NULL)";
		cmd.ExecuteNonQuery();
	}

	SqliteConnection Open()
	{
		var connection = new SqliteConnection(_connectionString);
		connection.Open();
		return connection;
	}
}
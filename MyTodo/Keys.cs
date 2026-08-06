namespace MyTodo;

public static class Keys
{
	public static class Todos
	{
		public static class Errors
		{
			public const string NO_TODOS = "No todos found";
			
			public const string INVALID_ID = "Invalid todo id";

			public const string REMOVE_TODO_NOT_FOUND = "Todo not found to be removed";
			public const string REMOVE_FAILED = "Failed to remove todo";

			public const string ADD_FAILED = "Failed to add todo";
			public const string TITLE_EMPTY = "Title is empty";

			public const string SAVE_FAILED = "Failed to save todos";
			public const string LOAD_FAILED = "Failed to load todos";
		}

		public static class Actions
		{
			public const string ADD = "add";
			public const string REMOVE = "remove";
			public const string LIST_TODOS = "list-todos";

			public const string SAVE = "save";
			public const string LOAD = "load";
		}
	}

	public static class UseCases
	{
		public static class Errors
		{
			public const string UNKNOWN_COMMAND = "Unknown command";
		}
	}
}
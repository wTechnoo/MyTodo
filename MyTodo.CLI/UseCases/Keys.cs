namespace MyTodo.CLI.UseCases;

public static class Keys
{
	public static class Actions
	{
		public const string CHANGE_PERSISTENCE = "change-persistence";
		public const string CHANGE_AUTO_PERSIST = "change-auto-persist";
		public const string EXIT = "exit";
		
		public const string LIST = "list";
	}

	public static class Errors
	{
		public const string INVALID_PERSISTENCE_TYPE = "Invalid persistence type.";
		public const string FAILED_TO_CHANGE_CONFIG = "Failed to alter a value in the config";

		public const string FAILED_TO_EXIT = "Failed when exiting program";
	}
}
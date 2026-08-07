namespace Infrastructure;

public static class Keys
{
	public const string AUTO_PERSIST = "auto-persist";
	public const string PERSISTENCE_TYPE = "persistence-type";

	public static class PersistenceTypes
	{
		public const string JSON = "json";
		public const string SQLITE = "sqlite";

		public static readonly IReadOnlyList<string> All = [JSON, SQLITE];
	}
}
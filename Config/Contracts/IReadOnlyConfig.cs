namespace Tools.Config.Contracts
{
    public interface IReadOnlyConfig : IEnumerable<(string, string)>
    {
        string Get(string key, string fallback = null);
    }
}

using System.Globalization;
using System.Reflection;
using System.Text;

namespace MyTodo.UseCases;

public abstract class BaseUseCases
{
	static readonly Dictionary<Type, MethodInfo[]> MethodsByType = new();
	protected IErrorMessenger? _errorMessenger;

	MethodInfo[] UseCaseMethods => MethodsOf(GetType());

	public async Task Receive(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return;

		var (action, args) = Parse(input);

		var useCase = UseCaseMethods.FirstOrDefault(m => string.Equals(ActionOf(m), action, StringComparison.OrdinalIgnoreCase));

		if (useCase == null)
		{
			_errorMessenger?.SendError(Keys.UseCases.Errors.UNKNOWN_COMMAND);
			return;
		}

		var arguments = useCase.GetParameters().Select(p => Bind(p, args)).ToArray();

		switch (useCase.Invoke(this, arguments))
		{
			case IAsyncCommand asyncCommand:
				await asyncCommand.ExecuteAsync();
				break;
			case ICommand command:
				command.Execute();
				break;
		}
	}

	static object Bind(ParameterInfo parameter, IReadOnlyDictionary<string, string> args)
	{
		var hasValue = args.TryGetValue(parameter.Name.ToLowerInvariant(), out var raw);

		if (parameter.ParameterType == typeof(int))
			return hasValue && int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i) ? i : Fallback(parameter, 0);

		if (parameter.ParameterType == typeof(float))
			return hasValue && float.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var f) ? f : Fallback(parameter, 0f);

		if (parameter.ParameterType == typeof(bool))
			return hasValue && bool.TryParse(raw, out var b) ? b : Fallback(parameter, false);

		return hasValue ? raw : Fallback(parameter, string.Empty);
	}

	static (string action, Dictionary<string, string> args) Parse(string input)
	{
		var tokens = Tokenize(input);

		var action = tokens.Count > 0 ? tokens[0] : string.Empty;

		var args = new Dictionary<string, string>();

		for (var i = 1; i < tokens.Count; i++)
		{
			var eq = tokens[i].IndexOf('=');

			if (eq > 0)
				args[tokens[i].Substring(0, eq).ToLowerInvariant()] = tokens[i].Substring(eq + 1);
		}

		return (action, args);
	}

	static List<string> Tokenize(string input)
	{
		var tokens = new List<string>();
		var current = new StringBuilder();
		var inQuotes = false;
		var quoteChar = '\0';

		foreach (var character in input)
		{
			if (!inQuotes && character is '"' or '\'')
			{
				inQuotes = true;
				quoteChar = character;
			}
			else if (inQuotes && character == quoteChar)
			{
				inQuotes = false;
				quoteChar = '\0';
			}
			else if (!inQuotes && char.IsWhiteSpace(character))
			{
				if (current.Length > 0)
				{
					tokens.Add(current.ToString());
					current.Clear();
				}
			}
			else
			{
				current.Append(character);
			}
		}

		if (current.Length > 0)
			tokens.Add(current.ToString());

		return tokens;
	}

	static object Fallback(ParameterInfo parameter, object fallback)
	{
		return parameter.HasDefaultValue ? parameter.DefaultValue : fallback;
	}

	static MethodInfo[] MethodsOf(Type type)
	{
		if (MethodsByType.TryGetValue(type, out var methods))
			return methods;

		return MethodsByType[type] = type
			.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
			.Where(m => m.GetCustomAttribute<UseCaseAttribute>() != null)
			.ToArray();
	}

	public IReadOnlyList<string> Verbs()
	{
		return MethodsOf(GetType()).Select(ActionOf).ToArray();
	}

	static string ActionOf(MethodInfo method)
	{
		return method.GetCustomAttribute<UseCaseAttribute>().Action;
	}
}
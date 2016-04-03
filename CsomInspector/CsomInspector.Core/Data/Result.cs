using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsomInspector.Core
{
	public class Result
	{
		protected Result(Int32 actionId, IEnumerable<ResultProperty> properties)
		{
			ActionId = actionId;
			Properties = properties;
		}

		public static IEnumerable<Result> FromJson(IEnumerable<JToken> tokens)
		{
			var resultsCount = tokens.Count() / 2;

			for (int i = 0; i < resultsCount; i++)
			{
				var actionId = tokens
					.Skip(i * 2)
					.First()
					.Value<Int32>();

				var resultToken = tokens
					.Skip(i * 2)
					.Skip(1)
					.First();

				var properties = ResultProperty.FromJson(resultToken.Children());

				yield return new Result(actionId, properties);
			}
		}

		public Int32 ActionId { get; private set; }

		public IEnumerable<ResultProperty> Properties { get; private set; }
	}

	public class ResultProperty : IObjectTreeNode
	{
		protected ResultProperty(String name, String value)
		{
			Name = name;
			Value = value;
		}

		protected ResultProperty(String name, IEnumerable<IObjectTreeNode> children)
		{
			Name = name;
			Children = children;
		}

		public IEnumerable<IObjectTreeNode> Children { get; private set; }

		public String Name { get; private set; }

		public String Value { get; private set; }

		public override String ToString()
		{
			if (!String.IsNullOrWhiteSpace(Value))
			{
				return $"{Name}: {Value}";
			}

			return $"{Name}";
		}

		public static IEnumerable<ResultProperty> FromJson(JEnumerable<JToken> tokens)
		{
			foreach (var token in tokens)
			{
				var name = token.Path;
				var child = token.Children().FirstOrDefault();
				var value = child?.ToString();

				yield return new ResultProperty(name, value);
			}
		}
	}
}
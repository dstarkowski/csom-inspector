using CsomInspector.Core.ObjectPaths;
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

		public static Result FromJson(JToken actionIdToken, JToken resultToken)
		{
			var actionId = actionIdToken.Value<Int32>();
			var properties = ResultProperty
				.FromJson(resultToken.Children())
				.ToList();

			return new Result(actionId, properties);
		}

		public Int32 ActionId { get; }

		public IEnumerable<ResultProperty> Properties { get; }
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

		public IEnumerable<IObjectTreeNode> Children { get; }

		public String Name { get; }

		public String Value { get; }

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
				var property = token as JProperty;
				var child = token.Children().FirstOrDefault();
				var value = child?.ToString();

				if (String.Equals(property.Name, "_ObjectIdentity_", StringComparison.InvariantCultureIgnoreCase))
				{
					var identity = IdentityParameter.FromIdentityString(value);

					yield return new ResultProperty(property.Name, identity);
				}
				else
				{
					yield return new ResultProperty(property.Name, value);
				}
			}
		}
	}
}
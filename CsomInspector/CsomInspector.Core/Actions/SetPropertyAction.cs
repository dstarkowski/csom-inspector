using CsomInspector.Core.ObjectPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class SetPropertyAction : ActionBase
	{
		private SetPropertyAction(String name, Parameter parameter)
		{
			PropertyName = name;
			Parameter = parameter;
		}

		public override IEnumerable<IObjectTreeNode> Children =>
			Parameter.Value == null ? new[] { Parameter } : Enumerable.Empty<IObjectTreeNode>();

		public Parameter Parameter { get; }

		public String PropertyName { get; }

		public override String ToString()
		{
			var arguments = String.Empty;

			if (Parameter.Value != null)
			{
				arguments = Parameter.ToString();
			}
			else
			{
				arguments = "...";
			}

			return $".{PropertyName} = {arguments}";
		}

		internal static new SetPropertyAction FromXml(XElement actionElement)
		{
			var nameAttribute = actionElement.Attribute(XName.Get("Name"));
			var parameterElements = actionElement.Elements(XName.Get("Parameter", _elementNamespace));

			var parameter = Parameter
				.FromXml(parameterElements)
				.FirstOrDefault();

			return new SetPropertyAction(nameAttribute.Value, parameter);
		}
	}
}
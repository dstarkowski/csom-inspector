using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Method : ObjectPath
	{
		private Method(Int32 id, String name, IEnumerable<Parameter> parameters)
		{
			Name = name;
			Parameters = parameters;
		}

		public override IEnumerable<IObjectTreeNode> Children => Parameters.Any(p => p.Value == null) ? Parameters : Enumerable.Empty<IObjectTreeNode>();

		public String Name { get; }

		public IEnumerable<Parameter> Parameters { get; }

		public override String ToString()
		{
			var arguments = String.Empty;

			if (Parameters.All(p => p.Value != null))
			{
				arguments = String.Join(", ", Parameters.Select(p => p));
			}
			else
			{
				arguments = String.Join(", ", Parameters.Select(p => "..."));
			}

			return $".{Name}({arguments})";
		}

		internal static new Method FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));

			var parametersElement = element.Element(XName.Get("Parameters", _elementNamespace));

			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			if (parametersElement != null)
			{
				var parameterElements = parametersElement.Elements(XName.Get("Parameter", _elementNamespace));
				var parameters = Parameter.FromXml(parameterElements);

				return new Method(id, nameAttribute.Value, parameters);
			}

			return new Method(id, nameAttribute.Value, Enumerable.Empty<Parameter>());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Method : ObjectPath
	{
		private Method(String name, IEnumerable<Parameter> parameters)
		{
			Name = name;
			Parameters = parameters;
		}

		public override String Type => "Instance method";
		public String Name { get; private set; }

		public IEnumerable<Parameter> Parameters { get; private set; }

		public override IEnumerable<IObjectTreeNode> Children => Parameters.Any(p => p.Value == null) ? Parameters : Enumerable.Empty<IObjectTreeNode>();

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

		internal static Method FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));

			var parameterElements = element.Element(XName.Get("Parameters", _elementNamespace))?.Elements(XName.Get("Parameter", _elementNamespace));
			var parameters = Parameter.FromXml(parameterElements);

			return new Method(nameAttribute.Value, parameters);
		}
	}
}
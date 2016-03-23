using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class StaticMethod : ObjectPath
	{
		private StaticMethod(String name, String typeId, IEnumerable<Parameter> parameters)
		{
			Name = name;
			TypeId = Guid.Parse(typeId);
			Parameters = parameters;
		}

		public override String Type => "Static method";
		public String Name { get; private set; }
		public Guid TypeId { get; private set; }
		public String TypeName => TypeMappings.Current.Get(TypeId);

		public IEnumerable<Parameter> Parameters { get; private set; }

		public override IEnumerable<IObjectTreeNode> Children => Parameters;

		public override String ToString()
		{
			var arguments = String.Join(", ", Parameters.Select(p => "..."));

			return $"{TypeName}.{Name}({arguments})";
		}

		internal static StaticMethod FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));
			var typeAttribute = element.Attribute(XName.Get("TypeId"));

			var parameterElements = element.Element(XName.Get("Parameters", _elementNamespace))?.Elements(XName.Get("Parameter", _elementNamespace));

			var parameters = Enumerable.Empty<Parameter>();
			if (parameterElements != null)
			{
				var paremeters = Parameter.FromXml(parameterElements);
			}

			return new StaticMethod(nameAttribute.Value, typeAttribute.Value, parameters);
		}
	}
}
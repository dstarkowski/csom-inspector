using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class StaticMethod : ObjectPath
	{
		private StaticMethod(Int32 id, String name, String typeId, IEnumerable<Parameter> parameters)
		{
			Name = name;
			TypeId = Guid.Parse(typeId);
			Parameters = parameters;
		}

		public override IEnumerable<IObjectTreeNode> Children => Parameters;

		public String Name { get; }

		public IEnumerable<Parameter> Parameters { get; }

		public Guid TypeId { get; }

		public String TypeName => TypeMappings.Current.Get(TypeId);

		public override String ToString()
		{
			var arguments = String.Join(", ", Parameters.Select(p => "..."));

			return $"{TypeName}.{Name}({arguments})";
		}

		internal static new StaticMethod FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));
			var typeAttribute = element.Attribute(XName.Get("TypeId"));

			var parameterElements = element.Element(XName.Get("Parameters", _elementNamespace))?.Elements(XName.Get("Parameter", _elementNamespace));

			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			var parameters = Enumerable.Empty<Parameter>();
			if (parameterElements != null)
			{
				var paremeters = Parameter.FromXml(parameterElements);
			}

			return new StaticMethod(id, nameAttribute.Value, typeAttribute.Value, parameters);
		}
	}
}
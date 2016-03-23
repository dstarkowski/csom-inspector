using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Parameter : IObjectTreeNode
	{
		protected const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		private Parameter(String type, String value)
		{
			Type = type;
			Value = value;
			Properties = Enumerable.Empty<ParameterProperty>();
		}

		private Parameter(String type, IEnumerable<ParameterProperty> properties)
		{
			Type = type;
			Properties = properties;
		}

		public IEnumerable<ParameterProperty> Properties { get; private set; }

		public IEnumerable<IObjectTreeNode> Children => Properties;

		public String Type { get; private set; }
		public String Value { get; private set; }

		public override String ToString()
		{
			if (Value == null)
			{
				return $"new {Type}()";
			}

			if (String.Equals(Type, "String", StringComparison.InvariantCultureIgnoreCase))
			{
				return $"\"{Value}\"";
			}

			return $"[{Type}] {Value}";
		}

		public static IEnumerable<Parameter> FromXml(IEnumerable<XElement> elements)
		{
			foreach (var element in elements)
			{
				var typeAttribute = element.Attribute(XName.Get("Type"));
				if (typeAttribute != null)
				{
					yield return new Parameter(typeAttribute.Value, element.Value);
				}
				else
				{
					var typeIdAttribute = element.Attribute(XName.Get("TypeId"));
					var typeName = TypeMappings.Current.Get(Guid.Parse(typeIdAttribute.Value));

					var propertyElements = element.Elements(XName.Get("Property", _elementNamespace));
					var properties = ParameterProperty.FromXml(propertyElements);

					yield return new Parameter(typeName, properties);
				}
			}
		}
	}

	public class ParameterProperty : IObjectTreeNode
	{
		private ParameterProperty(String name, String type, String value)
		{
			Name = name;
			Type = type;
			Value = value;
		}

		public IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public String Name { get; private set; }
		public String Type { get; private set; }
		public String Value { get; private set; }

		public override String ToString() => $"{Name} = [{Type}] {Value}";

		public static IEnumerable<ParameterProperty> FromXml(IEnumerable<XElement> elements)
		{
			foreach (var element in elements)
			{
				var nameAttribute = element.Attribute(XName.Get("Name"));
				var typeAttribute = element.Attribute(XName.Get("Type"));

				var type = typeAttribute?.Value;

				if (type != null && !String.Equals(type, "Null", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return new ParameterProperty(nameAttribute.Value, type, element.Value);
				}
			}
		}
	}
}
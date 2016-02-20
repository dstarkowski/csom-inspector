using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public abstract class ObjectPath : IObjectTreeNode
	{
		protected const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		protected ObjectPath()
		{
		}

		public abstract String Type { get; }

		public virtual IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public override String ToString() => Type;

		public static IEnumerable<ObjectPath> FromXml(IEnumerable<XElement> pathElements, Int32 pathId)
		{
			var results = new List<ObjectPath>();

			while (pathId >= 0)
			{
				var element = pathElements.Single(e => e.Attribute(XName.Get("Id"))?.Value == pathId.ToString());

				results.Insert(0, CreatePath(element));

				var parentAttribute = element.Attribute(XName.Get("ParentId"));
				pathId = parentAttribute == null ? -1 : Convert.ToInt32(parentAttribute.Value);
			}

			return results;
		}

		private static ObjectPath CreatePath(XElement element)
		{
			switch (element.Name.LocalName)
			{
				case "Property":
					return Property.FromXml(element);
				case "StaticProperty":
					return StaticProperty.FromXml(element);
				case "Method":
					return Method.FromXml(element);
				case "StaticMethod":
					return StaticMethod.FromXml(element);
				case "Constructor":
					return Constructor.FromXml(element);
				default:
					return GenericObjectPath.FromXml(element);
			}
		}
	}
}
using CsomInspector.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public abstract class ObjectPath : ActionBase
	{
		public static ObjectPath FromXml(XElement pathElement, XElement actionElement)
		{
			var pathIdAttribute = pathElement.Attribute(XName.Get("Id"));
			var actionIdAttribute = actionElement?.Attribute(XName.Get("Id"));

			var path = CreatePath(pathElement);
			path.ObjectPathId = Convert.ToInt32(pathIdAttribute.Value);

			if (actionIdAttribute != null)
			{
				path.Id = Convert.ToInt32(actionIdAttribute.Value);
			}

			return path;
		}

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
				case "Identity":
					return Identity.FromXml(element);
				default:
					return GenericObjectPath.FromXml(element);
			}
		}
	}
}
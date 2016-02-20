using CsomInspector.Core.ObjectPaths;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class Action : IObjectTreeNode
	{
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		protected Action(String name)
		{
			Name = name;
			Path = new List<ObjectPath>();
		}

		public virtual IEnumerable<IObjectTreeNode> Children => Path;

		public String Name { get; private set; }

		public IEnumerable<ObjectPath> Path { get; private set; }

		public static Action FromXml(XElement actionElement, IEnumerable<XElement> objectPaths)
		{
			if (actionElement == null)
			{
				throw new ArgumentNullException(nameof(actionElement));
			}

			var name = actionElement.Name;
			if (actionElement.Name.NamespaceName != _elementNamespace)
			{
				throw new ArgumentException("Specified element does not match CSOM request namespace.", nameof(actionElement));
			}

			var id = actionElement.Attribute(XName.Get("Id"));
			var objectPathId = actionElement.Attribute(XName.Get("ObjectPathId"));

			var action = CreateAction(actionElement);
			action.Path = ObjectPath.FromXml(objectPaths, Convert.ToInt32(objectPathId.Value));

			return action;
		}

		private static Action CreateAction(XElement element)
		{
			switch (element.Name.LocalName)
			{
				case "Query":
					return Query.FromXml(element);
				default:
					return new Action(element.Name.LocalName);
			}
		}

		public override String ToString() => $"Unrecognized action '{Name}'";
	}
}
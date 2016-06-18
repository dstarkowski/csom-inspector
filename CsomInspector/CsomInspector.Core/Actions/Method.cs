using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class Method : Action
	{
		private Method(ObjectPaths.Method method, IEnumerable<IObjectTreeNode> attributes, IEnumerable<IObjectTreeNode> elements)
			: base("Method")
		{
			_children = new[] { method }
				.Concat(attributes)
				.Concat(elements)
				.ToList();
		}

		protected const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		private IEnumerable<IObjectTreeNode> _children;

		public override IEnumerable<IObjectTreeNode> Children
		{
			get
			{
				if (Path != null && Path.Any())
				{
					return _children.Concat(new[] {
						new ObjectTreeNode("Target (ObjectPath element)", Path)
					});
				}
				else {
					return _children;
				}
			}
		}

		public override String ToString() => $"{Name}";

		internal static Method FromXml(XElement actionElement)
		{
			var nameAttribute = actionElement.Attribute(XName.Get("Name"));
			var parametersElement = actionElement.Element(XName.Get("Parameters"));
			var method = ObjectPaths.Method.FromXml(actionElement);

			var attributes = actionElement
				.Attributes()
				.Where(a => a.Name.LocalName != "Id" && a.Name.LocalName != "ObjectPathId" && a.Name.LocalName != "Name")
				.Select(a => new ObjectTreeNode($"{a.Name.LocalName} = {a.Value}", Enumerable.Empty<IObjectTreeNode>()));

			var elements = actionElement
				.Elements()
				.Where(e => e.Name.LocalName != "Parameters" && e.Name.LocalName != "Parameter")
				.Select(e => GenericAction.FromXml(e));

			return new Method(method, attributes, elements);
		}
	}
}
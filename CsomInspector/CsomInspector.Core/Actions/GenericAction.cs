using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class GenericAction : Action
	{
		private GenericAction(String name, IEnumerable<IObjectTreeNode> attributes, IEnumerable<IObjectTreeNode> elements) : base(name)
		{
			_children = attributes
				.Concat(elements)
				.ToList();
		}

		private IEnumerable<IObjectTreeNode> _children;

		public override IEnumerable<IObjectTreeNode> Children => _children.Concat(new[] {
			new ObjectTreeNode("Target (ObjectPath element)", Path)
		});

		public override String ToString() => $"{Name} (?)";

		internal static GenericAction FromXml(XElement actionElement)
		{
			var attributes = actionElement
				.Attributes()
				.Where(a => a.Name.LocalName != "Id" && a.Name.LocalName != "ObjectPathId")
				.Select(a => new ObjectTreeNode($"{a.Name.LocalName} = {a.Value}", Enumerable.Empty<IObjectTreeNode>()));

			var elements = actionElement
				.Elements()
				.Select(e => FromXml(e));

			return new GenericAction(actionElement.Name.LocalName, attributes, elements);
		}
	}
}
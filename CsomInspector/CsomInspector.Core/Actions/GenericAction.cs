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

		public override String ToString() => $"{Name} (?)";

		internal static GenericAction FromXml(XElement actionElement)
		{
			var name = actionElement.Name.LocalName;

			var attributes = actionElement
				.Attributes()
				.Where(a => a.Name.LocalName != "Id" && a.Name.LocalName != "ObjectPathId")
				.Select(a => new ObjectTreeNode($"{a.Name.LocalName} = {a.Value}", Enumerable.Empty<IObjectTreeNode>()));

			var elements = actionElement
				.Elements()
				.Select(e => FromXml(e));

			if (elements.Any())
			{
				return new GenericAction(name, attributes, elements);
			}

			var value = actionElement.Value;
			if (!String.IsNullOrWhiteSpace(value))
			{
				var valueNode = new[] {
					new ObjectTreeNode($"Value = {value}", Enumerable.Empty<IObjectTreeNode>())
				};
				return new GenericAction(name, attributes, valueNode);
			}

			return new GenericAction(name, attributes, Enumerable.Empty<IObjectTreeNode>());
        }
	}
}
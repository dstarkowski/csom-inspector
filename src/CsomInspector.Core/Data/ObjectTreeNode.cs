using System;
using System.Collections.Generic;

namespace CsomInspector.Core
{
	public interface IObjectTreeNode
	{
		IEnumerable<IObjectTreeNode> Children { get; }
	}

	public class ObjectTreeNode : IObjectTreeNode
	{
		public ObjectTreeNode(String name, IEnumerable<IObjectTreeNode> children)
		{
			Name = name;
			Children = children;
		}

		public IEnumerable<IObjectTreeNode> Children { get; set; }

		public String Name { get; set; }

		public override String ToString() => Name;
	}
}
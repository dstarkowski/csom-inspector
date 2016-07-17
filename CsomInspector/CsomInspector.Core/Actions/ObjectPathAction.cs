using CsomInspector.Core.ObjectPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class ObjectPathAction : Action
	{
		private ObjectPathAction(String name)
			: base(name)
		{
		}

		protected const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		public override IEnumerable<IObjectTreeNode> Children { get; }

		public override String ToString() => $"{Name}";

		internal static ObjectPathAction FromXml(XElement actionElement, IEnumerable<ObjectPath> paths)
		{
			var path = paths.Last();

			return new ObjectPathAction(path.ToString());
		}
	}
}
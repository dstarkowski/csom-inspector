using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Constructor : ObjectPath
	{
		private Constructor(String typeId)
		{
			TypeId = Guid.Parse(typeId);
		}

		public String TypeName => TypeMappingHelper.GetTypeName(TypeId);
		public Guid TypeId { get; private set; }

		public override String Type => "Constructor";

		public override IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public override String ToString() => $"new {TypeName}()";

		internal static Constructor FromXml(XElement element)
		{
			var typeAttribute = element.Attribute(XName.Get("TypeId"));

			return new Constructor(typeAttribute.Value);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Constructor : ObjectPath
	{
		private Constructor(Int32 id, String typeId)
			: base(id)
		{
			TypeId = Guid.Parse(typeId);
		}

		public String TypeName => TypeMappings.Current.Get(TypeId);
		public Guid TypeId { get; private set; }

		public override String Type => "Constructor";

		public override IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public override String ToString() => $"new {TypeName}()";

		internal static Constructor FromXml(XElement element)
		{
			var typeAttribute = element.Attribute(XName.Get("TypeId"));
			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			return new Constructor(id, typeAttribute.Value);
		}
	}
}
using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Constructor : ObjectPath
	{
		private Constructor(Int32 id, String typeId)
		{
			TypeId = Guid.Parse(typeId);
		}

		public Guid TypeId { get; }

		public String TypeName => TypeMappings.Current.Get(TypeId);

		public override String ToString() => $"new {TypeName}()";

		internal static new Constructor FromXml(XElement element)
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
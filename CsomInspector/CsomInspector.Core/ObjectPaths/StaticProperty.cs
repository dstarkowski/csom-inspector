using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class StaticProperty : ObjectPath
	{
		private StaticProperty(Int32 id, String name, String typeId)
			: base(id)
		{
			Name = name;
			TypeId = Guid.Parse(typeId);
		}

		public override String Type => "Static property";
		public String Name { get; private set; }
		public Guid TypeId { get; private set; }
		public String TypeName => TypeMappings.Current.Get(TypeId);

		public override String ToString() => $"{TypeName}.{Name}";

		internal static StaticProperty FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));
			var typeAttribute = element.Attribute(XName.Get("TypeId"));
			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			return new StaticProperty(id, nameAttribute.Value, typeAttribute.Value);
		}
	}
}
using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Property : ObjectPath
	{
		private Property(Int32 id, String name)
			: base(id)
		{
			Name = name;
		}

		public override String Type => "Instance property";

		public String Name { get; }

		public override String ToString() => $".{Name}";

		internal static Property FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));
			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			return new Property(id, nameAttribute.Value);
		}
	}
}
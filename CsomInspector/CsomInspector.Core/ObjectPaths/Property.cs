using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Property : ObjectPath
	{
		private Property(String name)
		{
			Name = name;
		}

		public override String Type => "Instance property";
		public String Name { get; private set; }

		public override String ToString() => $".{Name}";

		internal static Property FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));

			return new Property(nameAttribute.Value);
		}
	}
}
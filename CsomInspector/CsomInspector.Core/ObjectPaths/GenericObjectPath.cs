using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class GenericObjectPath : ObjectPath
	{
		private GenericObjectPath(Int32 id, String type)
		{
			_type = type;
		}

		private String _type;

		public override String ToString() => $"Unrecognized action '{_type}'";

		internal static new GenericObjectPath FromXml(XElement element)
		{
			var type = element.Name.LocalName;
			var idValue = element
				.Attribute(XName.Get("Id"))
				.Value;
			var id = Convert.ToInt32(idValue);

			return new GenericObjectPath(id, type);
		}
	}
}
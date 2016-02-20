using System;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class GenericObjectPath : ObjectPath
	{
		private GenericObjectPath(String type)
		{
			_type = type;
		}

		private String _type;
		public override String Type => _type;

		public override String ToString() => $"Unrecognized action '{_type}'";

		internal static GenericObjectPath FromXml(XElement element)
		{
			var type = element.Name.LocalName;

			return new GenericObjectPath(type);
		}
	}
}
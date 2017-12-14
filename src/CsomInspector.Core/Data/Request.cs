using System;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Request
	{
		private Request(String applicationName, String libraryVersion, String clientTag)
		{
			ApplicationName = applicationName ?? String.Empty;
			ClientTag = clientTag;
			LibraryVersion = new Version(libraryVersion) ?? new Version();
		}

		private const String _elementName = "Request";

		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		public String ApplicationName { get; }

		public Version LibraryVersion { get; }

		public String ClientTag { get; }

		public static Request FromXml(XElement element, String clientTag)
		{
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			var name = element.Name;
			if (element.Name.LocalName != _elementName || element.Name.NamespaceName != _elementNamespace)
			{
				throw new ArgumentException("Specified element does not match CSOM request name or namespace.", nameof(element));
			}

			var applicationName = element.Attribute(XName.Get("ApplicationName"));
			var libraryVersion = element.Attribute(XName.Get("LibraryVersion"));

			var request = new Request(applicationName?.Value, libraryVersion?.Value, clientTag);

			return request;
		}
	}
}
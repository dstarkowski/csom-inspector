using System;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Request
	{
		private const String _elementName = "Request";
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		private Request(String applicationName, String libraryVersion, String schemaVersion)
		{
			ApplicationName = applicationName ?? String.Empty;
			LibraryVersion = new Version(libraryVersion) ?? new Version();
			SchemaVersion = new Version(schemaVersion) ?? new Version();
		}

		public String ApplicationName { get; private set; }

		public Version LibraryVersion { get; private set; }

		public String VersionDisplayName
		{
			get
			{
				switch (LibraryVersion.Major)
				{
					case 14:
						return "SharePoint 2010";
					case 15:
						return "SharePoint 2013";
					case 16:
						return "SharePoint Online";
					default:
						return "Other";
				}
			}
		}

		public Version SchemaVersion { get; private set; }

		public static Request FromXml(XElement element)
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
			var schemaVersion = element.Attribute(XName.Get("SchemaVersion"));

			var request = new Request(applicationName?.Value, libraryVersion?.Value, schemaVersion?.Value);

			return request;
		}
	}
}
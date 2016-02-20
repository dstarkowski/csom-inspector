using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Inspector
	{
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		public Inspector(String requestData)
		{
			_document = XDocument.Parse(requestData);
		}

		private XDocument _document;

		public Request GetRequestData()
		{
			var rootElement = _document.Root;
			return Request.FromXml(rootElement);
		}

		public IEnumerable<Actions.Action> GetActionsData()
		{
			var actionsElement = _document.Root.Element(XName.Get("Actions", _elementNamespace));
			var objectPathsElement = _document.Root.Element(XName.Get("ObjectPaths", _elementNamespace));
			foreach (var action in actionsElement.Elements())
			{
				yield return Actions.Action.FromXml(action, objectPathsElement.Elements());
			}
		}
	}
}
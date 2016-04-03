using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Inspector
	{
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		public Inspector(String requestBody, String responseBody)
		{
			_request = XDocument.Parse(requestBody);
			_response = JArray.Parse(responseBody);
		}

		private XDocument _request;
		private JArray _response;

		public Request GetRequestData()
		{
			var rootElement = _request.Root;
			return Request.FromXml(rootElement);
		}

		public IEnumerable<Actions.Action> GetActionsData()
		{
			var actionsElement = _request.Root.Element(XName.Get("Actions", _elementNamespace));
			var objectPathsElement = _request.Root.Element(XName.Get("ObjectPaths", _elementNamespace));
			foreach (var action in actionsElement.Elements())
			{
				yield return Actions.Action.FromXml(action, objectPathsElement.Elements());
			}
		}

		public Response GetResponseData()
		{
			var responseElement = _response[0];
			return Response.FromJson(responseElement);
		}

		public IEnumerable<Result> GetResultsData()
		{
			var resultsElements = _response.Skip(1);
			var results = Result.FromJson(resultsElements);

			return results.ToList();
		}
	}
}
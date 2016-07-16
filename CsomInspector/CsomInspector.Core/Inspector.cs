using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Inspector
	{
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";
		private const String _clientTagHeader = "X-ClientService-ClientTag";

		public Inspector(String requestBody, String responseBody, IDictionary<String, String> requestHeaders)
		{
			_requestHeaders = requestHeaders;
			_request = XDocument.Parse(requestBody);
			try
			{
				_response = JArray.Parse(responseBody);
			}
			catch
			{
				_response = new JArray();
			}
		}

		private XDocument _request;
		private JArray _response;
		private IDictionary<String, String> _requestHeaders;

		public Request GetRequestData()
		{
			var rootElement = _request.Root;
			var clientTag = _requestHeaders.ContainsKey(_clientTagHeader) ? _requestHeaders[_clientTagHeader] : String.Empty;

			return Request.FromXml(rootElement, clientTag);
		}

		public IEnumerable<Actions.Action> GetActionsData()
		{
			var actionsElement = _request.Root.Element(XName.Get("Actions", _elementNamespace));
			var objectPathsElement = _request.Root.Element(XName.Get("ObjectPaths", _elementNamespace));
			foreach (var action in actionsElement.Elements())
			{
				if (action.Name.LocalName != "ExceptionHandlingScope")
				{
					yield return Actions.Action.FromXml(action, objectPathsElement.Elements());
				}
				else
				{
					yield return Actions.ExceptionHandlingScope.FromXml();
				}
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
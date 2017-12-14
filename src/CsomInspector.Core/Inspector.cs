using CsomInspector.Core.Actions;
using CsomInspector.Core.ObjectPaths;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core
{
	public class Inspector
	{
		public Inspector(String requestBody, String responseBody, IDictionary<String, String> requestHeaders, TimeSpan sessionTime)
		{
			_requestHeaders = requestHeaders;
			_request = XDocument.Parse(requestBody);
			_sessionTime = sessionTime;

			try
			{
				_response = JArray.Parse(responseBody);
			}
			catch
			{
				_response = new JArray();
			}
		}

		private const String _clientTagHeader = "X-ClientService-ClientTag";
		private const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";
		private XDocument _request;
		private IDictionary<String, String> _requestHeaders;
		private JArray _response;
		private TimeSpan _sessionTime;

		public List<ActionBase> GetActionsData()
		{
			var results = ParseResults();
			var actions = ParseActions();
			var objectPaths = ParseObjectPaths();

			return MergeActions(actions, objectPaths, results);
		}

		public Request GetRequestData()
		{
			var rootElement = _request.Root;
			var clientTag = _requestHeaders.ContainsKey(_clientTagHeader) ? _requestHeaders[_clientTagHeader] : String.Empty;

			var request = Request.FromXml(rootElement, clientTag);

			return request;
		}

		public Response GetResponseData()
		{
			var responseElement = _response[0];
			return Response.FromJson(responseElement);
		}

		private List<ActionBase> MergeActions(IEnumerable<ActionBase> actions, IEnumerable<ActionBase> objectPaths, IEnumerable<Result> results)
		{
			var mergedActions = actions
				.Concat(objectPaths)
				.OrderBy(action => action.Id)
				.ToList();

			foreach (var action in mergedActions)
			{
				action.Results = results
					.Where(r => r.ActionId == action.Id || action.MergedActions.Any(a => a.Id == r.ActionId))
					.ToList();
			}

			return mergedActions;
		}

		private IEnumerable<ActionBase> ParseActions()
		{
			var actionElements = _request.Root
				.Element(XName.Get("Actions", _elementNamespace))
				.Elements()
				.Where(element => element.Name.LocalName != "ObjectPath")
				.Where(element => element.Name.LocalName != "ExceptionHandlingScope")//TODO: Temp
				.ToList();

			foreach (var element in actionElements)
			{
				yield return ActionBase.FromXml(element);
			}
		}

		private IEnumerable<ActionBase> ParseObjectPaths()
		{
			var elements = _request.Root
				.Element(XName.Get("Actions", _elementNamespace))
				.Elements();

			var actionElements = elements
				.Where(element => element.Name.LocalName == "ObjectPath");

			var objectPathElements = _request.Root
				.Element(XName.Get("ObjectPaths", _elementNamespace))
				.Elements();

			foreach (var objectPath in objectPathElements)
			{
				var id = objectPath
					.Attribute(XName.Get("Id"))
					.Value;

				var action = actionElements
					.Where(a => a.Attribute(XName.Get("ObjectPathId")).Value == id)
					.SingleOrDefault();

				yield return ObjectPath.FromXml(objectPath, action);
			}
		}

		private Result[] ParseResults()
		{
			var resultsCount = (_response.Count - 1) / 2;
			var results = new Result[resultsCount];

			for (var i = 0; i < resultsCount; i++)
			{
				var actionIdToken = _response[i * 2 + 1];
				var resultToken = _response[i * 2 + 2];

				results[i] = Result.FromJson(actionIdToken, resultToken);
			}

			return results;
		}
	}
}
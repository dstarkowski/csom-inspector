using CsomInspector.Core;
using CsomInspector.Fiddler.View;
using Fiddler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CsomInspector.Fiddler.Presentation
{
	public class InspectorPresenter
	{
		public InspectorPresenter()
		{
			RequestViewModel = new RequestViewModel();
			RequestInfoViewModel = new RequestInfoViewModel();

			View = new InspectorView();
			View.DataContext = this;
		}

		public RequestViewModel RequestViewModel { get; }

		public RequestInfoViewModel RequestInfoViewModel { get; }

		public InspectorView View { get; }

		public void SetSession(Session[] sessions)
		{
			var state = ValidateSessions(sessions);

			if (state == InspectorState.Single)
			{
				InspectSession(sessions);
			}
			else
			{
			}
		}

		private void InspectSession(Session[] sessions)
		{
			var session = sessions[0];
			var requestBody = session.GetRequestBodyAsString();
			var responseBody = session.GetResponseBodyAsString();
			var requestHeaders = GetHeaders(session.RequestHeaders);

			try
			{
				var inspector = new Inspector(requestBody, responseBody, requestHeaders);
				var actions = inspector.GetActionsData();
				var requestData = inspector.GetRequestData();
				var responseData = inspector.GetResponseData();
				var results = inspector.GetResultsData();

				RequestViewModel.Actions = new ObservableCollection<Core.Actions.Action>(actions);
				RequestViewModel.Results = results;
				RequestViewModel.ErrorInfo = responseData.ErrorInfo;
				RequestInfoViewModel.SetSessionData(requestData, responseData);
			}
			catch
			{
				RequestViewModel.Actions = new ObservableCollection<Core.Actions.Action>();
				RequestViewModel.Results = Enumerable.Empty<Core.Result>();
				RequestInfoViewModel.SetSessionData(null, null);
			}
		}

		private Dictionary<String, String> GetHeaders(HTTPRequestHeaders headers)
		{
			var dictionary = new Dictionary<String, String>(headers.Count());

			foreach (var header in headers)
			{
				dictionary.Add(header.Name, header.Value);
			}

			return dictionary;
		}

		private InspectorState ValidateSessions(Session[] sessions)
		{
			if (sessions == null || sessions.Length == 0)
			{
				return InspectorState.None;
			}

			if (sessions.Length > 1)
			{
				return InspectorState.Multiple;
			}

			if (ValidateSession(sessions[0]))
			{
				return InspectorState.Single;
			}

			return InspectorState.Incorrect;
		}

		//TODO: needs additional validation
		private Boolean ValidateSession(Session session)
		{
			var contentType = session.RequestHeaders["Content-Type"];

			return String.Equals(contentType, "text/xml");
		}
	}
}
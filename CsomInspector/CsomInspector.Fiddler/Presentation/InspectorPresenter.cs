using System;
using CsomInspector.Fiddler.View;
using Fiddler;
using CsomInspector.Core;
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

		public RequestViewModel RequestViewModel { get; private set; }
		public RequestInfoViewModel RequestInfoViewModel { get; private set; }

		public InspectorView View { get; private set; }

		public void SetSession(Session[] sessions)
		{
			var state = ValidateSessions(sessions);

			if (state == InspectorState.Single)
			{
				//View.ShowInspector();
				InspectSession(sessions);
			}
			else {
				//View.ShowError(state);
			}
		}

		private void InspectSession(Session[] sessions)
		{
			var session = sessions[0];
			var requestBody = session.GetRequestBodyAsString();
			var responseBody = session.GetResponseBodyAsString();

			try
			{
				var inspector = new Inspector(requestBody, responseBody);
				var actions = inspector.GetActionsData();
				var requestData = inspector.GetRequestData();
				var responseData = inspector.GetResponseData();
				var results = inspector.GetResultsData();

				RequestViewModel.Actions = actions;
				RequestViewModel.Results = results;
				RequestInfoViewModel.SetSessionData(requestData, responseData);
			}
			catch
			{
				RequestViewModel.Actions = Enumerable.Empty<Core.Actions.Action>();
				RequestViewModel.Results = Enumerable.Empty<Core.Result>();
				RequestInfoViewModel.SetSessionData(null, null);
			}
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
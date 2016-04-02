using System;
using CsomInspector.Fiddler.View;
using Fiddler;

namespace CsomInspector.Fiddler.Presentation
{
	public class InspectorPresenter
	{
		public InspectorPresenter()
		{
			RequestViewModel = new RequestViewModel();

			View = new InspectorView();
			View.DataContext = this;
		}

		public RequestViewModel RequestViewModel { get; private set; }

		public InspectorView View { get; private set; }

		public void SetSession(Session[] sessions)
		{
			var state = ValidateSessions(sessions);

			if (state == InspectorState.Single)
			{
				//View.ShowInspector();
				RequestViewModel.SetSession(sessions[0]);
			}
			else {
				//View.ShowError(state);
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
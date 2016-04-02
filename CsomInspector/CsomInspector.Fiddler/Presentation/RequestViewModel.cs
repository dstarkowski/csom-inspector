using CsomInspector.Core;
using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestViewModel : INotifyPropertyChanged
	{
		public RequestViewModel()
		{
		}

		private IEnumerable<Core.Actions.Action> _actions;

		private Core.Actions.Action _selectedAction;

		public event PropertyChangedEventHandler PropertyChanged;

		public IEnumerable<Core.Actions.Action> Actions
		{
			get
			{
				return _actions;
			}
			private set
			{
				_actions = value;
				RaisePropertyChanged(nameof(Actions));
			}
		}

		public Core.Actions.Action SelectedAction
		{
			get
			{
				return _selectedAction;
			}
			set
			{
				_selectedAction = value;
				RaisePropertyChanged(nameof(SelectedAction));
			}
		}

		public void InspectElement(String body)
		{
			var inspector = new Inspector(body);
			Actions = inspector.GetActionsData();
		}

		public void SetSession(Session session)
		{
			var body = session.GetRequestBodyAsString();

			InspectElement(body);
		}

		private void RaisePropertyChanged(String propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
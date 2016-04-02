using System.Collections.Generic;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestViewModel : ViewModelBase
	{
		public RequestViewModel()
		{
		}

		private IEnumerable<Core.Actions.Action> _actions;

		private Core.Actions.Action _selectedAction;

		public IEnumerable<Core.Actions.Action> Actions
		{
			get
			{
				return _actions;
			}
			set
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
	}
}
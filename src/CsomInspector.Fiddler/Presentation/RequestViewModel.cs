using CsomInspector.Core;
using CsomInspector.Core.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestViewModel : ViewModelBase
	{
		private ObservableCollection<ActionBase> _actions;
		private ErrorInfo _errorInfo;
		private ActionBase _selectedAction;

		public ObservableCollection<ActionBase> Actions
		{
			get
			{
				return _actions;
			}
			set
			{
				_actions = value;
				RaisePropertyChanged(nameof(Actions));
				SelectedAction = _actions.FirstOrDefault();
			}
		}

		public ErrorInfo ErrorInfo
		{
			get
			{
				return _errorInfo;
			}
			set
			{
				_errorInfo = value;
				RaisePropertyChanged(nameof(Results));
			}
		}

		public IEnumerable<IObjectTreeNode> Results
		{
			get
			{
				if (ErrorInfo != null)
				{
					return new[] { ErrorInfo };
				}

				return SelectedAction?.Results?
					.SelectMany(result => result.Properties);
			}
		}

		public ActionBase SelectedAction
		{
			get
			{
				return _selectedAction;
			}
			set
			{
				_selectedAction = value;

				HighlightActions(_selectedAction);

				RaisePropertyChanged(nameof(Actions));
				RaisePropertyChanged(nameof(SelectedAction));
				RaisePropertyChanged(nameof(Results));
			}
		}

		public IEnumerable<Result> SelectedResult => SelectedAction?.Results;

		private void HighlightActions(ActionBase selectedAction)
		{
			if (selectedAction == null)
			{
				return;
			}

			var pathIds = new List<Int32>();
			pathIds.Add(selectedAction.ObjectPathId);

			foreach (var action in _actions.Reverse())
			{
				var highlight = pathIds.Contains(action.ObjectPathId);
				if (highlight)
				{
					pathIds.Add(action.ObjectPathId);
				}

				action.Highlight(highlight);
			}
		}
	}
}
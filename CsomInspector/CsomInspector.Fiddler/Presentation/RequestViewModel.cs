using CsomInspector.Core;
using System.Collections.Generic;
using System.Linq;
using Action = CsomInspector.Core.Actions.Action;
using CsomInspector.Core.Actions;
using System;
using System.Collections.ObjectModel;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestViewModel : ViewModelBase
	{
		public RequestViewModel()
		{
		}

		private ObservableCollection<Action> _actions;
		private IEnumerable<Result> _results;
		private Action _selectedAction;

		public ObservableCollection<Action> Actions
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

		public IEnumerable<Result> Results
		{
			get
			{
				return _results;
			}
			set
			{
				_results = value;
				RaisePropertyChanged(nameof(Results));
				RaisePropertyChanged(nameof(ResultView));
			}
		}

		public Action SelectedAction
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
				RaisePropertyChanged(nameof(ResultView));
			}
		}

		private void HighlightActions(Action selectedAction)
		{
			var selectedPaths = selectedAction.Path;

			foreach (var action in _actions)
			{
				var lastPath = action.Path.LastOrDefault();
				if (lastPath != null)
				{
					var isHighlighted = selectedPaths.Any(path => path.Id == lastPath.Id);
					action.Highlight(isHighlighted);
				}
			}
		}

		public Result SelectedResult
		{
			get
			{
				if (SelectedAction != null)
				{
					return Results
						.Where(r => r.ActionId == SelectedAction.Id)
						.FirstOrDefault();
				}

				return null;
			}
		}

		private ErrorInfo _errorInfo;
		public ErrorInfo ErrorInfo
		{
			get
			{
				return _errorInfo;
			}
			set
			{
				_errorInfo = value;
				RaisePropertyChanged(nameof(ResultView));
			}
		}

		public IEnumerable<IObjectTreeNode> ResultView
		{
			get
			{
				if (ErrorInfo != null)
				{
					return new[] { ErrorInfo };
				}

				return SelectedResult?.Properties;
			}
		}
	}
}
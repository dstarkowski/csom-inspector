using CsomInspector.Core;
using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CsomInspector.Fiddler
{
	public class RequestViewModel : INotifyPropertyChanged
	{
		public RequestViewModel()
		{
			Text = "EMPTY";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(String propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public String Text { get; set; }

		public void SetBody(Byte[] body, HTTPRequestHeaders headers)
		{
			var contentType = headers["Content-Type"];

			if (String.Equals(contentType, "text/xml"))
			{
				var bodyString = Encoding.UTF8.GetString(body);

				InspectElement(bodyString);
			}
			else
			{
				Request = null;
				Actions = null;
			}

			RaisePropertyChanged(nameof(Request));
			RaisePropertyChanged(nameof(Actions));
		}

		public Request Request { get; set; }
		public IEnumerable<IObjectTreeNode> Actions { get; set; }

		private readonly String[] _automaticActions = new[] { "ObjectPath", "ObjectIdentityQuery" };

		public void InspectElement(String body)
		{
			var inspector = new Inspector(body);
			Request = inspector.GetRequestData();
			var actions = inspector.GetActionsData().GroupBy(g => g.Name);

			var manualActions = new List<IObjectTreeNode>();
			var automaticActions = new List<IObjectTreeNode>();

			foreach (var group in actions)
			{
				if (_automaticActions.Contains(group.Key))
				{
					automaticActions.AddRange(group);
				}
				else
				{
					manualActions.AddRange(group);
				}
			}

			Actions = new[] {
				new ObjectTreeNode("Requested actions", manualActions),
				new ObjectTreeNode("Other actions", automaticActions)
			};
		}
	}
}
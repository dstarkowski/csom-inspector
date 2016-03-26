using Fiddler;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CsomInspector.Fiddler
{
	public class Extension : IFiddlerExtension
	{
		private RequestViewModel _viewModel;

		public Byte[] body
		{
			get
			{
				return null;
			}
			set
			{
				_viewModel.SetBody(value, headers);
			}
		}

		public HTTPRequestHeaders headers { get; set; }

		public void OnLoad()
		{
			var view = new RequestView();
			_viewModel = new RequestViewModel();
			view.DataContext = _viewModel;

			var host = new ElementHost();
			host.Dock = DockStyle.Fill;
			host.Child = view;

			var tab = new TabPage("CSOM inspector");
			tab.Controls.Add(host);

			FiddlerApplication.UI.tabsViews.TabPages.Add(tab);
			FiddlerApplication.CalculateReport += OnSelectionChanged;
		}

		private void OnSelectionChanged(Session[] sessions)
		{
			if (sessions != null && sessions.Length == 1)
			{
				headers = sessions[0].RequestHeaders;
				body = sessions[0].RequestBody;
			}
		}

		public void OnBeforeUnload()
		{
		}
	}
}
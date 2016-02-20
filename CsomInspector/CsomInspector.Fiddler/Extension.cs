using Fiddler;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CsomInspector.Fiddler
{
	public class Extension : Inspector2, IRequestInspector2
	{
		private RequestViewModel _viewModel;

		public Boolean bDirty => false;

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

		public Boolean bReadOnly { get; set; }

		public HTTPRequestHeaders headers { get; set; }

		public override void AddToTab(TabPage tab)
		{
			var view = new RequestView();
			_viewModel = new RequestViewModel();
			view.DataContext = _viewModel;

			var host = new ElementHost();
			host.Dock = DockStyle.Fill;
			host.Child = view;

			tab.Text = "CSOM inspector";
			tab.Controls.Add(host);
		}

		public void Clear()
		{
		}

		public override Int32 GetOrder() => 1000;
	}
}
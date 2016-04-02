using CsomInspector.Fiddler.Presentation;
using Fiddler;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CsomInspector.Fiddler
{
	public class Extension : IFiddlerExtension
	{
		private InspectorPresenter _presenter;

		public void OnBeforeUnload()
		{
		}

		public void OnLoad()
		{
			_presenter = new InspectorPresenter();

			var host = new ElementHost();
			host.Dock = DockStyle.Fill;
			host.Child = _presenter.View;

			var tab = new TabPage("CSOM inspector");
			tab.Controls.Add(host);

			FiddlerApplication.UI.tabsViews.TabPages.Add(tab);
			FiddlerApplication.CalculateReport += OnSesionChanged;
		}

		private void OnSesionChanged(Session[] sessions)
		{
			_presenter.SetSession(sessions);
		}
	}
}
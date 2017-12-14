using CsomInspector.Fiddler.Presentation;
using Fiddler;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CsomInspector.Fiddler
{
	public class Extension : IFiddlerExtension
	{
		public Extension()
		{
			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblies;
		}

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

			FiddlerApplication.UI.tabsViews.TabPages.Insert(2, tab);
			FiddlerApplication.CalculateReport += OnSesionChanged;
		}

		private void OnSesionChanged(Session[] sessions)
		{
			_presenter.SetSession(sessions);
		}

		private Assembly ResolveAssemblies(Object sender, ResolveEventArgs args)
		{
			var assemblyName = new AssemblyName(args.Name);
			var resourceName = $"CsomInspector.Fiddler.Dependencies.{assemblyName.Name}.dll";
			var assembly = Assembly.GetExecutingAssembly();

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			{
				var assemblyData = new Byte[stream.Length];
				stream.Read(assemblyData, 0, assemblyData.Length);

				return Assembly.Load(assemblyData);
			}
		}
	}
}
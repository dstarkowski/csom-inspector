using System;
using System.ComponentModel;

namespace CsomInspector.Fiddler.Presentation
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(params String[] propertyNames)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				foreach (var property in propertyNames)
				{
					handler(this, new PropertyChangedEventArgs(property));
				}
			}
		}
	}
}
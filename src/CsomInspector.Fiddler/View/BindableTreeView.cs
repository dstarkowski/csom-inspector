using System;
using System.Windows;
using System.Windows.Controls;

namespace CsomInspector.Fiddler.View
{
	public class BindableTreeView : TreeView
	{
		public BindableTreeView()
			: base()
		{
			SelectedItemChanged += (sender, e) => SelectedItemBinding = SelectedItem;
		}

		public static readonly DependencyProperty SelectedItemBindingProperty = DependencyProperty.Register("SelectedItemBinding", typeof(Object), typeof(BindableTreeView), new UIPropertyMetadata());

		public Object SelectedItemBinding
		{
			get
			{
				return GetValue(SelectedItemBindingProperty);
			}
			set
			{
				SetValue(SelectedItemBindingProperty, value);
			}
		}
	}
}